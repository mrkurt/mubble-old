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
using System.Collections.Generic;
using Mubble.Models;

namespace Mubble.Web.Admin
{

    public partial class Article : Mubble.UI.AdminPage
    {
        protected int CurrentPageNumber = 1;
        protected bool ContentFromCache = false;
        protected Controller featuredContent;

        /// <summary>
        /// Gets or sets the working featured content object
        /// </summary>
        public Controller FeaturedContent
        {
            get
            {
                if (featuredContent == null && Page.IsPostBack)
                {
                    featuredContent = Cache[this.WorkingCacheKey] as Controller;
                    if (featuredContent != null)
                    {
                        this.ContentFromCache = true;
                    }
                }
                if (featuredContent == null)
                {
                    featuredContent = this.Controller;
                    if (Page.IsPostBack)
                    {
                        Cache.Insert(
                            this.WorkingCacheKey, 
                            featuredContent, 
                            null, 
                            DateTime.MaxValue, 
                            new TimeSpan(24, 0, 0), 
                            System.Web.Caching.CacheItemPriority.NotRemovable,
                            null
                            );
                    }
                }
                return featuredContent;
            }
            set
            {
                featuredContent = value;
                if (Page.IsPostBack)
                {
                    Cache.Insert(
                        this.WorkingCacheKey, 
                        featuredContent, null, 
                        DateTime.MaxValue, 
                        new TimeSpan(24, 0, 0), 
                        System.Web.Caching.CacheItemPriority.NotRemovable,
                        null                        
                        );
                }
            }
        }

        /// <summary>
        /// Gets the cache key for the working content
        /// </summary>
        protected string WorkingCacheKey
        {
            get
            {
                if (this.Controller != null)
                {
                    //TODO: keep track of various cache key types
                    return string.Format(
                        "UserAction-{0}-Editing-FeaturedContent-{1}",
                        User.Identity.Name,
                        this.Controller.Path
                        );
                }
                return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.AssertPermission("full", "publish", "draft");
            this.PreRender += new EventHandler(Article_PreRender);
            if (!Page.IsPostBack)
            {
                Cache.Remove(this.WorkingCacheKey);
                this.CurrentPageNumber = 1;
                txtTitle.Text = Controller.Title;
                txtContentBody.Text = Controller.Body;
                txtExcerpt.Text = Controller.Excerpt;

                if (this.FeaturedContent.Pages.Count == 0)
                {
                    Mubble.Models.Page blank = new Mubble.Models.Page();
                    blank.PageNumber = 1;
                    blank.Name = "Page 1";
                    blank.Body = "This is page 1";
                    this.FeaturedContent.AddPage(blank);
                    this.FeaturedContent.Save();
                }
                #region Initialize date crapola
                this.lstMonth.SelectedValue = Controller.PublishDate.Month.ToString();
                for (int i = 1; i <= 31; i++)
                {
                    this.lstDay.Items.Add(i.ToString());
                }
                this.lstDay.SelectedValue = Controller.PublishDate.Day.ToString();

                for (int i = 1995; i <= DateTime.Now.Year + 10; i++)
                {
                    this.lstYear.Items.Add(i.ToString());
                }
                this.lstYear.SelectedValue = Controller.PublishDate.Year.ToString();

                for (int i = 1; i <= 12; i++)
                {
                    ListItem item = new ListItem();
                    item.Value = i.ToString();
                    item.Text = (i < 10) ? "0" + i.ToString() : i.ToString();
                    item.Selected = (i == Controller.PublishDate.Hour || i + 12 == Controller.PublishDate.Hour);

                    this.lstHour.Items.Add(item);
                }

                for (int i = 0; i <= 59; i++)
                {
                    ListItem item = new ListItem();
                    item.Value = i.ToString();
                    item.Text = (i < 10) ? "0" + i.ToString() : i.ToString();
                    item.Selected = (i == Controller.PublishDate.Minute);

                    this.lstMinute.Items.Add(item);
                }

                this.lstAmPm.SelectedValue = (Controller.PublishDate.Hour >= 12) ? "PM" : "AM";

                #endregion
            }
            Mubble.UI.Admin.ActiveObjectFieldEditor.BindActiveObjectEditors(this, this.FeaturedContent);
        }

        /// <summary>
        /// Fires when most of the page processing should be done, sets various values, etc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Article_PreRender(object sender, EventArgs e)
        {
            if (this.CurrentPageNumber > this.FeaturedContent.Pages.Count)
            {
                this.CurrentPageNumber = this.FeaturedContent.Pages.Count;
            }
            Mubble.Models.Page workingPage = this.FeaturedContent.GetPage(this.CurrentPageNumber);
            this.SetPageData(workingPage);
        }
        protected void AddPage_Click(object sender, System.EventArgs e)
        {
            this.UpdatePageText();
            Mubble.Models.Page newPage = new Mubble.Models.Page();
            newPage.PageNumber = this.FeaturedContent.Pages.Count + 1;
            newPage.Body = string.Format("Page {0} text.", newPage.PageNumber);
            newPage.Name = string.Format("Page {0}", newPage.PageNumber);
            this.FeaturedContent.AddPage(newPage);
            this.CurrentPageNumber = newPage.PageNumber;
        }
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            this.UpdatePageText();
            if (Page.IsValid)
            {
                LinkButton button = sender as LinkButton;
                if (button.CommandArgument == "Draft")
                {
                    this.FeaturedContent.Status = PublishStatus.Draft;
                }
                if (button.CommandArgument == "Publish")
                {
                    this.FeaturedContent.Status = PublishStatus.Published;
                }
                this.FeaturedContent.Title = this.txtTitle.Text;
                this.FeaturedContent.Body = Mubble.Config.Caching.ResolveMediaPaths(this.txtContentBody.Text);
                this.FeaturedContent.Excerpt = this.txtExcerpt.Text;
                this.FeaturedContent.PublishDate = DateTime.Parse(
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
                this.FeaturedContent.Save();

                Response.Redirect(Request.RawUrl);
            }
        }
        protected void SetPageData(Mubble.Models.Page workingPage)
        {
            this.txtTitle.Text = this.FeaturedContent.Title;
            if (workingPage != null && workingPage.Body != null)
            {
                this.txtBody.Text = workingPage.Body;
            }
            
            this.PageList.DataSource = this.FeaturedContent.Pages.Sort("PageNumber");
            this.PageList.DataBind();

            this.CurrentPageNumber = workingPage.PageNumber;

            this.Title = (this.FeaturedContent.DataManager.IsDirty) ? "[Unsaved] " : "";
            this.Title += string.Format("Editing \"{0}\"", this.FeaturedContent.Title);
        }
        protected void PageSelector_Click(object sender, System.EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            if (btn != null)
            {
                this.UpdatePageText();
                int pageNumber = 1;
                int.TryParse(btn.CommandArgument, out pageNumber);

                this.CurrentPageNumber = pageNumber;
            }
        }

        protected void PageUp_Click(object sender, System.EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            if (btn != null)
            {
                this.UpdatePageText();

                int pageNumber = 1;
                int.TryParse(btn.CommandArgument, out pageNumber);

                this.FeaturedContent.MovePage(pageNumber, pageNumber - 1);

                this.CurrentPageNumber = pageNumber - 1;
            }
        }

        protected void PageDown_Click(object sender, System.EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            if (btn != null)
            {
                this.UpdatePageText();

                int pageNumber = 1;
                int.TryParse(btn.CommandArgument, out pageNumber);

                this.FeaturedContent.MovePage(pageNumber, pageNumber + 1);

                this.CurrentPageNumber = pageNumber + 1;
            }
        }

        protected void PageDelete_Click(object sender, EventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            if (btn != null)
            {
                this.UpdatePageText();

                int pageNumber = 1;
                int.TryParse(btn.CommandArgument, out pageNumber);

                this.FeaturedContent.RemovePage(pageNumber);
                this.CurrentPageNumber = (pageNumber > 1) ? pageNumber - 1 : 1;
            }
        }

        protected void UpdatePageText()
        {
            this.FeaturedContent.Title = this.txtTitle.Text;
            foreach (RepeaterItem item in this.PageList.Items)
            {
                TextBox name = item.FindControl("txtPageName") as TextBox;
                HiddenField pageNum = item.FindControl("txtPageNumber") as HiddenField;
                if (name != null && pageNum != null && name.Visible)
                {
                    int pageNumber = -1;
                    int.TryParse(pageNum.Value, out pageNumber);
                    Mubble.Models.Page workingPage = this.FeaturedContent.GetPage(pageNumber);

                    if (workingPage == null)
                    {
                        workingPage = new Mubble.Models.Page();
                        workingPage.PageNumber = pageNumber;
                        this.FeaturedContent.AddPage(workingPage);
                    }
                    workingPage.Name = name.Text;
                    workingPage.Body = Mubble.Config.Caching.ResolveMediaPaths(this.txtBody.Text);
                }
            }
        }
    }
}