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

    public partial class Section : Mubble.UI.AdminPage
    {
        protected Mubble.Models.Controllers.Section CurrentSection { get { return this.Controller as Mubble.Models.Controllers.Section; } }
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

                Controller.Settings["ModuleId"] = this.modulePicker.ModuleId;
                Controller.Settings["Control"] = this.modulePicker.Control;

                if (this.CurrentSection != null && (this.CurrentSection.Discussion == null ||
                    this.CurrentSection.Discussion.DiscussionProviderID != new Guid(this.lstDiscussionProvider.SelectedValue)))
                {
                    this.CurrentSection.Discussion = new Discussion();
                    this.CurrentSection.Discussion.DiscussionProviderID = new Guid(this.lstDiscussionProvider.SelectedValue);
                    this.CurrentSection.Discussion.Status = DiscussionStatus.Inactive;
                    this.CurrentSection.Discussion.CommentCount = 0;
                }

                Controller.Save();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.AssertPermission("full");
            if (!Page.IsPostBack)
            {
                if (this.Controller.Settings["ModuleId"] == null)
                {
                    this.Controller.Settings["ModuleId"] = this.Controller.ModuleControl.ModuleID;
                }
                this.txtTitle.Text = Controller.Title;
                this.txtBody.Text = Controller.Body;

                this.lstDiscussionProvider.DataSource = DiscussionProvider.Find().Sort("IsDefault DESC");
                this.lstDiscussionProvider.DataTextField = "Name";
                this.lstDiscussionProvider.DataValueField = "ID";


                if (this.CurrentSection != null && this.CurrentSection.Discussion != null && this.CurrentSection.Discussion.ID != Guid.Empty)
                {
                    this.lstDiscussionProvider.SelectedValue = this.CurrentSection.Discussion.DiscussionProviderID.ToString();
                }
            }
            else
            {
                this.Controller.Settings["ModuleId"] = this.modulePicker.ModuleId;
                this.Controller.Settings["Control"] = this.modulePicker.Control;
            }
        }

    }
}