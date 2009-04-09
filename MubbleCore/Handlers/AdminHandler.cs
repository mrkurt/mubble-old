using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Mubble.UI;
using System.Xml.Serialization;
using System.IO;
using System.Web.Configuration;
using Mubble.Models;
using Mubble.Models.QueryEngine;
using System.Web.Compilation;

namespace Mubble.Handlers
{
    /// <summary>
    /// Handler responsible for requests to Admin pages
    /// </summary>
    public class AdminHandler : HttpHandler
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override bool UseCachedController
        {
            get { return false; }
        }

        public override void ProcessMubbleRequest(System.Web.HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                Security.User.RedirectToLogin();
            }

            if (Mubble.Config.Administration.Current.NewStyle)
            {
                log.Info("Loading *new* style admin interface for " + context.Request.RawUrl);
                string pagePath = "~/Admin/Controller.aspx";
                string moduleSpecificPagePath = Mubble.Tools.Path.Combine(
                    this.Controller.ModuleControl.Module.Path,
                    "Admin/",
                    this.Controller.ModuleControl.FileName
                );

                string templatePagePath = pagePath.Replace("~/", Mubble.Config.Administration.Current.TemplatePath);
                string templateModuleSpecificPagePath = moduleSpecificPagePath.Replace("~/", Mubble.Config.Administration.Current.TemplatePath);

                if (System.IO.File.Exists(context.Server.MapPath(templateModuleSpecificPagePath)))
                {
                    pagePath = templateModuleSpecificPagePath;
                }
                else if (System.IO.File.Exists(context.Server.MapPath(templatePagePath)))
                {
                    pagePath = templatePagePath;
                }
                else if (System.IO.File.Exists(context.Server.MapPath(moduleSpecificPagePath)))
                {
                    pagePath = moduleSpecificPagePath;
                }

                AdminPage handler = (AdminPage)BuildManager.CreateInstanceFromVirtualPath(pagePath, typeof(Mubble.UI.AdminPage));

                handler.SetRequestVariables(this);
                handler.IsAdmin = true;

                handler.ProcessRequest(context);
                context.Response.Write(string.Format("<!-- Page Path: {0} -->", pagePath));
            }
            else
            {
                #region OldStyle
                log.Info("Loading old style admin interface for " + context.Request.RawUrl);
                string pagePath = "";

                bool pageFound = false;

                // This will hold the appropriate path item string

                string requestModule = this.Url.GetPathItem(0, "");

                if (requestModule.IndexOf("m-") == 0 && requestModule.Length > 3)
                {
                    this.Url.Action = requestModule.Substring(2);
                }
                else if (requestModule.Length > 0)
                {
                    this.Url.Action = requestModule;
                }
                if (this.Url.Action != null && this.Url.Action.Length > 0)
                {
                    string moduleName = this.Url.Action + ".aspx";

                    Mubble.Models.AdminControl adminControl = this.Controller.ModuleControl.Find(moduleName);

                    if (adminControl != null)
                    {
                        pagePath = this.Controller.ModuleControl.Module.Path + "Admin/" + adminControl.FileName;
                        pageFound = System.IO.File.Exists(context.Server.MapPath(pagePath));
                    }
                }
                if (this.Url.Action != null && this.Url.Action.Length > 0 && !pageFound)
                {
                    string moduleName = this.Url.Action + ".aspx";
                    pagePath = "~/Admin/" + moduleName;
                    pageFound = System.IO.File.Exists(context.Server.MapPath(pagePath));
                }
                if (this.Controller.ModuleControl.DefaultAdminControl != null && !pageFound)
                {
                    string moduleName = this.Controller.ModuleControl.DefaultAdminControl.FileName;

                    this.Url.Action = moduleName.Substring(0, moduleName.LastIndexOf('.')).ToLower();
                    requestModule = "m-" + this.Url.Action;
                    pagePath = this.Controller.ModuleControl.Module.Path + "Admin/" + this.Controller.ModuleControl.DefaultAdminControl.FileName;
                    pageFound = System.IO.File.Exists(context.Server.MapPath(pagePath));
                }

                if (!pageFound)
                {
                    requestModule = "browse";
                    string moduleName = "Browse.aspx";
                    pagePath = "~/Admin/" + moduleName;
                    pageFound = System.IO.File.Exists(context.Server.MapPath(pagePath));
                }

                if (pageFound)
                {
                    string templatePath = pagePath.Replace("~/", Mubble.Config.Administration.Current.TemplatePath);
                    if (System.IO.File.Exists(context.Server.MapPath(templatePath)))
                    {
                        pagePath = templatePath;
                    }
                }

                AdminPage handler = (AdminPage)BuildManager.CreateInstanceFromVirtualPath(pagePath, typeof(Mubble.UI.AdminPage));

                handler.SetRequestVariables(this);

                handler.Tab = requestModule;
                handler.ControlConfig = this.Controller.ModuleControl;
                handler.ModuleConfig = this.Controller.ModuleControl.Module;
                handler.IsAdmin = true;

                handler.ProcessRequest(context);
                #endregion
            }
        }

            
    }
}
