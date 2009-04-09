using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml;
using Mubble.UI;
using System.Web.Compilation;
using System.IO;
using Mubble.Models;

namespace Mubble.Handlers
{
    /// <summary>
    /// Handles requests for Web content
    /// </summary>
    public class HtmlHandler : HttpHandler
    {
        protected override bool ActionBeforeExtension
        {
            get { return false; }
        }
        public override void ProcessMubbleRequest(System.Web.HttpContext context)
        {
            string modulePath = this.Controller.ModuleControl.Module.Path + this.Controller.ModuleControl.FileName;
            string moduleViewPath = modulePath + "/.views" + this.Controller.ModuleControlView;
            string templatePath = modulePath.Replace("~/", this.Controller.Template.Path);
            string templateViewPath = templatePath + ".views/" + this.Controller.ModuleControlView;
            string customPath = Mubble.Tools.Path.Combine(this.Controller.Template.Path, "Pages/", this.Controller.Path.Substring(1)) + ".aspx";

            if (System.IO.File.Exists(context.Server.MapPath(customPath)))
            {
                modulePath = customPath;
            }
            else if (System.IO.File.Exists(context.Server.MapPath(templateViewPath)))
            {
                modulePath = templateViewPath;
            }
            else if (System.IO.File.Exists(context.Server.MapPath(templatePath)))
            {
                modulePath = templatePath;
            }
            else if (System.IO.File.Exists(context.Server.MapPath(moduleViewPath)))
            {
                modulePath = moduleViewPath;
            }

            Mubble.UI.Page handler = (Mubble.UI.Page)BuildManager.CreateInstanceFromVirtualPath(modulePath, typeof(Mubble.UI.Page));
            handler.SetCacheDependency(this.Controller);

            handler.SetRequestVariables(this);

            handler.ProcessRequest(context);
        }
    }
}
