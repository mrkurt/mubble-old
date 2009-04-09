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

    public partial class BlogSettings : Mubble.UI.AdminPage
    {
        public Mubble.Models.Controllers.Blog Blog
        {
            get { return this.Controller as Mubble.Models.Controllers.Blog; }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                LinkButton button = sender as LinkButton;
                if (button.CommandArgument == "Draft")
                {
                    Controller.Status = PublishStatus.Draft;
                }
                if (button.CommandArgument == "Publish")
                {
                    Controller.Status = PublishStatus.Published;
                }
                Controller.Title = this.txtTitle.Text;
                Controller.Body = this.txtBody.Text;
                if (Controller.PublishDate == DateTime.MinValue)
                {
                    Controller.PublishDate = DateTime.Now;
                }

                if (this.Blog.Discussion == null || 
                    this.Blog.Discussion.DiscussionProviderID != new Guid(this.lstDiscussionProvider.SelectedValue))
                {
                    this.Blog.Discussion = new Discussion();
                    this.Blog.Discussion.DiscussionProviderID = new Guid(this.lstDiscussionProvider.SelectedValue);
                    this.Blog.Discussion.Status = DiscussionStatus.Inactive;
                }
                if (Controller.Status == PublishStatus.Draft)
                {
                    this.Blog.Discussion.PublishDate = DateTime.MaxValue;
                }
                else
                {
                    this.Blog.Discussion.PublishDate = Controller.PublishDate;
                }


                this.Blog.MetaData.Clear("InlineDiscussions");
                this.Blog.MetaData.Set("InlineDiscussions", this.chckInlineDiscussions.Checked.ToString());

                Controller.Save();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.AssertPermission("full");

            Mubble.Models.Controllers.Blog blog = this.Controller as Mubble.Models.Controllers.Blog;
            if (!Page.IsPostBack)
            {
                this.txtTitle.Text = Controller.Title;
                this.txtBody.Text = Controller.Body;

                this.lstDiscussionProvider.DataSource = DiscussionProvider.Find().Sort("IsDefault DESC");
                this.lstDiscussionProvider.DataTextField = "Name";
                this.lstDiscussionProvider.DataValueField = "ID";

                if (this.Blog != null && this.Blog.Discussion != null && this.Blog.Discussion.ID != Guid.Empty)
                {
                    this.lstDiscussionProvider.SelectedValue = this.Blog.Discussion.DiscussionProviderID.ToString();
                }

                if (
                    this.Blog != null && 
                    true.ToString().Equals(this.Blog.MetaData.GetFirstStringValue("InlineDiscussions"), StringComparison.CurrentCultureIgnoreCase))
                {
                    this.chckInlineDiscussions.Checked = true;
                }
            }
        }

    }
}