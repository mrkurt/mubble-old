using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Mubble.Models;

namespace Mubble.Web.Admin
{
    public partial class Blog : Mubble.UI.AdminPage
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.AssertPermission("full", "post full", "publish", "draft");
        }
        private Post workingPost;
        protected Post WorkingPost
        {
            get
            {
                if(workingPost == null 
                    && "post".Equals(this.Url.GetPathItem(1), StringComparison.CurrentCultureIgnoreCase))
                {
                    workingPost = new Post();
                    string slug = Url.GetPathItem(2, null);
                    if (slug != null)
                    {
                        workingPost.Load(this.Controller.ID, slug);
                    }
                }
                return workingPost;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Url.Path == "/news") this.chckElevated.Visible = false;
            if (this.WorkingPost != null)
            {
                if (!Page.IsPostBack)
                {
                    Post p = this.WorkingPost;
                    DateTime publishDate = DateTime.Now;
                    this.txtUserName.Text = User.Identity.Name;
                    if (p.ID != Guid.Empty)
                    {
                        this.txtTitle.Text = p.Title;
                        this.txtSlug.Text = p.Slug;
                        this.txtBody.Text = p.Body;
                        this.txtExcerpt.Text = p.Excerpt;
                        this.txtUserName.Text = p.UserName;

                        publishDate = p.PublishDate;

                        this.btnPublishSave.Visible = !(p.Status == PublishStatus.Published);
                        this.btnDraftSave.Visible = !this.btnPublishSave.Visible;
                    }
                    #region Initialize date crapola
                    this.lstMonth.SelectedValue = publishDate.Month.ToString();
                    for (int i = 1; i <= 31; i++)
                    {
                        this.lstDay.Items.Add(i.ToString());
                    }
                    this.lstDay.SelectedValue = publishDate.Day.ToString();

                    for (int i = 1995; i <= DateTime.Now.Year + 10; i++)
                    {
                        this.lstYear.Items.Add(i.ToString());
                    }
                    this.lstYear.SelectedValue = publishDate.Year.ToString();

                    for (int i = 1; i <= 12; i++)
                    {
                        ListItem item = new ListItem();
                        item.Value = i.ToString();
                        item.Text = (i < 10) ? "0" + i.ToString() : i.ToString();
                        item.Selected = (i == publishDate.Hour || i + 12 == publishDate.Hour || (i == 12 && publishDate.Hour == 0));

                        this.lstHour.Items.Add(item);
                    }

                    for (int i = 0; i <= 59; i++)
                    {
                        ListItem item = new ListItem();
                        item.Value = i.ToString();
                        item.Text = (i < 10) ? "0" + i.ToString() : i.ToString();
                        item.Selected = (i == publishDate.Minute);

                        this.lstMinute.Items.Add(item);
                    }

                    this.lstAmPm.SelectedValue = (publishDate.Hour >= 12) ? "PM" : "AM";
                    #endregion
                }

                Mubble.UI.Admin.ActiveObjectFieldEditor.BindActiveObjectEditors(this, this.WorkingPost);
            }

            this.AuthorSelector.Visible = Mubble.Security.User.HasFlag(this.Controller, "full");

        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                LinkButton btn = sender as LinkButton;

                Post p = this.WorkingPost;

                p.PublishDate = DateTime.Parse(
                        string.Format(
                            "{0}/{1}/{2} {3}:{4} {5}",
                            this.lstMonth.SelectedValue,
                            this.lstDay.SelectedValue,
                            this.lstYear.SelectedValue,
                            this.lstHour.SelectedValue,
                            this.lstMinute.SelectedValue,
                            this.lstAmPm.SelectedValue
                        )
                    );
                if (p.ID == Guid.Empty)
                {
                    p.Status = PublishStatus.Draft;
                }

                if (btn != null && btn.CommandArgument == "Publish")
                {
                    p.Status = PublishStatus.Published;
                    if (p.PublishDate < DateTime.Now)
                    {
                        p.PublishDate = DateTime.Now;
                    }
                }
                else if (btn != null && btn.CommandArgument == "Draft")
                {
                    p.Status = PublishStatus.Draft;
                }

                p.ControllerID = this.Controller.ID;
                p.Title = this.txtTitle.Text;
                p.Slug = this.txtSlug.Text;
                p.Excerpt = this.txtExcerpt.Text;
                if (p.Slug == null || p.Slug.Trim().Length == 0)
                {
                    p.Slug = p.Title;
                }

                if (Mubble.Security.User.HasFlag(this.Controller, "full"))
                {
                    p.UserName = this.txtUserName.Text;
                }
                
                if (string.IsNullOrEmpty(p.UserName))
                {
                    p.UserName = this.User.Identity.Name;
                }
                p.Body = Mubble.Config.Caching.ResolveMediaPaths(this.txtBody.Text);

                p.Save();
                Response.Redirect(this.Url.ToString("AdminHandler", ""));
            }
        }
    }
}
