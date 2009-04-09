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
using Microsoft.Win32;
using Mubble.Models;
using System.Collections.Generic;

namespace Mubble.Web.Admin
{
    public partial class Settings : Mubble.UI.AdminPage
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.AssertPermission("full");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.txtFileName.Text = Controller.FileName;
                this.txtTitle.Text = Controller.Title;

                this.modulePicker.ModuleId = Controller.ModuleControl.ModuleID;
                this.modulePicker.Control = Controller.ModuleControl.ID;

                this.lstTemplates.DataSource = Template.Find();
                this.lstTemplates.DataValueField = "ID";
                this.lstTemplates.DataTextField = "Name";
                this.lstTemplates.DataBind();

                if (this.lstTemplates.Items.FindByValue(Controller.Template.ID.ToString()) != null)
                {
                    this.lstTemplates.SelectedValue = Controller.Template.ID.ToString();
                }
                else
                {
                    this.lstTemplates.SelectedIndex = 0;
                }
                this.LoadTemplateControls(Controller.Template.ID);

                string[] roles = Roles.GetAllRoles();
                this.lstGroups.DataSource = roles;
                this.lstGroups.DataBind();

                Dictionary<string, string> flags = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);

                flags.Add("view", "View content");
                flags.Add("view unpublished", "Preview unpublished content");
                flags.Add("draft", "Create draft content");
                flags.Add("publish", "Publish content");
                flags.Add("full", "Full control");

                foreach (PermissionFlag f in this.Controller.ModuleControl.Module.PermissionFlags)
                {
                    if (flags.ContainsKey(f.Flag))
                    {
                        flags[f.Flag] = f.Name;
                    }
                    else
                    {
                        flags.Add(f.Flag, f.Name);
                    }
                }

                this.lstPermissionsOptions.DataSource = flags;
                this.lstPermissionsOptions.DataBind();

                if (this.Controller.RssFeeds.Count > 0)
                {
                    RssFeed feed = this.Controller.RssFeeds[0];

                    this.txtRssTitle.Text = feed.Title;
                    this.txtRssLink.Text = feed.Link;
                    this.txtRssManagingEditor.Text = feed.ManagingEditor;
                    this.txtRssItemTemplate.Text = feed.ItemFormat;
                    this.txtRssDescription.Text = feed.Description;
                    this.txtRssRedirectUrl.Text = feed.RedirectUrl;
                    this.txtRssRedirectExceptions.Text = feed.RedirectExceptions;
                }
            }
        }

        protected void LoadTemplateControls(Guid templateId)
        {
            Template t = new Template(templateId);

            this.lstTemplateControls.DataSource = t.Config.Controls;
            this.lstTemplateControls.DataValueField = "FileName";
            this.lstTemplateControls.DataTextField = "Name";
            this.lstTemplateControls.DataBind();
        }

        protected void lstTemplates_Changed(object sender, System.EventArgs e)
        {
            this.LoadTemplateControls(new Guid(this.lstTemplates.SelectedValue));
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                this.Controller.Title = this.txtTitle.Text;

                this.Controller.FileName = this.txtFileName.Text;

                if (!this.txtFileName.Text.Equals(this.Controller.FileName, StringComparison.CurrentCultureIgnoreCase))
                {
                    //UrlRedirect redirect = new UrlRedirect();
                    //redirect.Url = this.Controller.Path + ".%";
                    //this.Controller.UrlRedirects.Add(redirect);
                }

                ModuleControl mc = ModuleControl.FindFirst(this.modulePicker.Control);

                this.Controller.ModuleControlID = mc.ID;
                this.Controller.ActiveObjectType = mc.ControllerActiveObjectType;

                this.Controller.TemplateID = new Guid(this.lstTemplates.SelectedValue);
                this.Controller.TemplateControl = this.lstTemplateControls.SelectedValue;

                this.Controller.Save();

                Models.Controller c = Models.Controller.FindFirst(this.Controller.ID);

                Response.Redirect(MubbleUrl.Url(c.Url.Path, "AdminHandler"));
            }
        }

        protected void RssSaveButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                RssFeed feed = new RssFeed();
                if (this.Controller.RssFeeds.Count > 0)
                {
                    feed = this.Controller.RssFeeds[0];
                }
                feed.Title = this.txtRssTitle.Text;
                feed.Link = this.txtRssLink.Text;
                feed.Slug = "";
                feed.ManagingEditor = this.txtRssManagingEditor.Text;
                feed.Description = this.txtRssDescription.Text;
                feed.ItemFormat = this.txtRssItemTemplate.Text;
                feed.RedirectUrl = this.txtRssRedirectUrl.Text;
                feed.RedirectExceptions = this.txtRssRedirectExceptions.Text;

                if (feed.ControllerID == Guid.Empty)
                {
                    feed.ControllerID = this.Controller.ID;
                }
                feed.Save();
            }
        }

        protected void RedirectSaveButton_Click(object sender, EventArgs e)
        {
            if (this.UrlRedirect_Url.Text != null && this.UrlRedirect_Url.Text.Length > 0)
            {
                UrlRedirect redirect = new UrlRedirect(this.UrlRedirect_Url.Text);
                redirect.Url = this.UrlRedirect_Url.Text;
                redirect.PathExtra = this.UrlRedirect_PathExtra.Text;
                if (redirect.PathExtra != null && redirect.PathExtra.Length > 0)
                {
                    redirect.PathExtra = "/" + redirect.PathExtra;
                }
                redirect.Handler = this.UrlRedirect_Handler.SelectedValue;
                this.Controller.UrlRedirects.Add(redirect);
                this.Controller.Save();
				foreach (UrlRedirect r in this.Controller.UrlRedirects)
				{
					r.Save();
				}
                this.UrlRedirect_PathExtra.Text = "";
                this.UrlRedirect_Url.Text = "";
                Response.Redirect(Request.RawUrl);
            }
        }
        protected void RedirectDelete_Click(object sender, EventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            if (btn != null)
            {
                Guid id = new Guid(btn.CommandArgument);
                this.Controller.UrlRedirects.Remove("ID", id);
                this.Controller.Save();
            }
        }
    }
}

