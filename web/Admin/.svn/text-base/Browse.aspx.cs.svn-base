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
using System.Collections.Generic;
using Mubble.Models;

namespace Mubble.Web.Admin
{
    public partial class Browse : Mubble.UI.AdminPage
    {
        private Dictionary<Guid, Module> modules = new Dictionary<Guid,Module>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.lstModules.DataSource = Module.Find();
                this.lstModules.DataValueField = "ID";
                this.lstModules.DataTextField = "Name";
                this.lstModules.DataBind();
                this.lstModules.SelectedIndex = 0;
                this.LoadModuleControls(new Guid(this.lstModules.SelectedValue));
            }
        }

        protected void LoadModuleControls(Guid moduleId)
        {
            Module m = new Module(moduleId);

            this.lstControls.DataSource = m.ModuleControls.Sort("Order");
            this.lstControls.DataValueField = "ID";
            this.lstControls.DataTextField = "Name";
            this.lstControls.DataBind();
        }

        protected void lstModules_Changed(object sender, System.EventArgs e)
        {
            this.LoadModuleControls(new Guid(this.lstModules.SelectedValue));
        }

        protected string GetModuleName(object dataItem)
        {
            Module m = getModule(dataItem);
            return m.ToString();
        }

        protected ModuleControl GetModuleControl(object dataItem, string control)
        {
            Mubble.Models.Module m = getModule(dataItem);

            return m.FindControl(control);
        }

        private Module getModule(object dataItem)
        {
            Mubble.Models.Controller c = dataItem as Mubble.Models.Controller;
            if (c == null) return null;
            if (modules.ContainsKey(c.ModuleControl.ModuleID))
            {
                return modules[c.ModuleControl.ModuleID];
            }
            else
            {
                modules.Add(c.ModuleControl.ModuleID, c.ModuleControl.Module);
                return c.ModuleControl.Module;
            }
        }

        protected void NewLocation_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            if (btn != null)
            {
                UrlRedirect redirect = new UrlRedirect();
                redirect.Url = this.Controller.Path + ".%";
                this.Controller.UrlRedirects.Add(redirect);

                this.Controller.ControllerID = new Guid(btn.CommandArgument);
                this.Controller.Save();
                this.MoveToSection.Visible = false;
                this.MoveToResult.Visible = true;
                this.MoveToResult.InnerText = string.Format("\"{0}\" successfully moved to \"{1}\"", this.Controller.FileName, this.Controller.Path);
            }
        }

        protected void SaveButton_Click(object sender, System.EventArgs e)
        {
            this.AssertPermission("full", "post full", "publish", "draft");
            if (Page.IsValid)
            {
                ModuleControl mc = ModuleControl.FindFirst(new Guid(this.lstControls.SelectedValue));

                Controller content = this.Controller.Clone() as Controller;
                content.Title = this.txtTitle.Text;
                content.FileName = this.txtFileName.Text;
                content.ControllerID = this.Controller.ID;
                content.ModuleControlView = null;
                content.ID = Guid.Empty;
                content.ModuleControlID = mc.ID;
                content.ActiveObjectType = mc.ControllerActiveObjectType;
                content.Status = PublishStatus.Draft;
                content.ModifyDate = DateTime.Now;
                content.PublishDate = DateTime.Now;
                this.Controller.Permissions.CopyTo(content.Permissions);

                content.Body = "";

                content.Save();

                content.Load(content.ID);

                Response.Redirect(MubbleUrl.Url(content.Path, "AdminHandler"));
            }
            else
            {
                foreach (System.Web.UI.IValidator validator in Validators)
                {
                    if (!validator.IsValid)
                    {
                        Response.Write(validator.ErrorMessage + " - Gah<br />");
                    }
                }
            }
        }
    }
}
