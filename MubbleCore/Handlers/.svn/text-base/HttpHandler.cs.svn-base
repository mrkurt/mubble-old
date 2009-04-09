using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;
using System.Text.RegularExpressions;
using Mubble.Models;
using System.Web.Security;
using System.Web;
using System.Web.Configuration;
using System.Web.Compilation;

namespace Mubble.Handlers
{
    /// <summary>
    /// Base Mubble class for handling HTTP requests.  The ParseRequest function should be called near the beginning of any
    /// new request.
    /// </summary>
    public abstract class HttpHandler : System.Web.IHttpHandler
    {
        public virtual bool UseCachedController
        {
            get { return true; }
        }


        private PathParameters pathParts = new PathParameters();

        public PathParameters PathParts
        {
            get { return pathParts; }
            set { pathParts = value; }
        }


        private Mubble.Models.ModuleControl controlConfig;

        /// <summary>
        /// The control configuration for this request
        /// </summary>
        public Mubble.Models.ModuleControl ControlConfig
        {
            get { return controlConfig; }
            set { controlConfig = value; }
        }

        private Controller content;

        /// <summary>
        /// The content object referenced by this request
        /// </summary>
        public Controller Controller
        {
            get { return content; }
            set { content = value; }
        }
	
        
        private MubbleUrl parsedUrl;

        /// <summary>
        /// The URL for the current request.
        /// </summary>
        public MubbleUrl Url
        {
            get { return parsedUrl; }
            set { parsedUrl = value; }
        }

        protected bool isReusable = false;

        protected virtual bool ActionBeforeExtension { get { return true; } }

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return this.isReusable; }
            set { this.isReusable = value; }
        }

        public virtual void ProcessRequest(System.Web.HttpContext context)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(context.Request.PhysicalPath);
            if (file.Exists)
            {
                IHttpHandler p = (System.Web.UI.Page)BuildManager.CreateInstanceFromVirtualPath(
                    context.Request.FilePath,
                    typeof(IHttpHandler)
                    );

                p.ProcessRequest(context);
                return;
            }
            this.ParseRequest(context);
            this.ProcessMubbleRequest(context);
        }

        public abstract void ProcessMubbleRequest(System.Web.HttpContext context);

        #endregion

        /// <summary>
        /// Parses relevant bits out of the current request URL.
        /// </summary>
        /// <param name="context">HttpContext of the current request</param>
        /// <returns>The ParsedUrl object with appropriate field values.</returns>
        protected void ParseRequest(System.Web.HttpContext context)
        {
            System.Web.HttpRequest Request = context.Request;

            MubbleUrl req = MubbleUrl.Parse(Request.Url, this.GetType().FullName, this.ActionBeforeExtension);

            string controllerKey = string.Format("Controller-{0}", req.Path);

            this.Controller = null;

            if (this.UseCachedController)
            {
                this.Controller = DataBroker.GetController(req.Path);
            }
            else
            {
                this.Controller = Controller.FindFirst(req.Path);
            }

            if (this.Controller == null || this.Controller.ID == Guid.Empty)
            {
                throw new System.Web.HttpException(404, "The specified content does not exist");
            }

            this.ControlConfig = this.Controller.ModuleControl;

            foreach (Route r in this.ControlConfig.Routes)
            {
                Regex regex = new Regex(r.Pattern, RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture);
                string cleanPathExtra = req.PathExtra != null ? req.PathExtra : "";

                cleanPathExtra = Regex.Replace(cleanPathExtra, @"^\/m-[^\/]*", "");
                
                Match m = regex.Match(cleanPathExtra);
                if (m.Success)
                {
                    this.PathParts = new PathParameters();
                    string[] groups = regex.GetGroupNames();
                    for (int i = 1; i < groups.Length; i++)
                    {
                        if (m.Groups[i].Value != "")
                        {
                            this.PathParts.Add(groups[i], HttpUtility.UrlDecode(m.Groups[groups[i]].Value));
                        }
                        else
                        {
                            this.PathParts.Add(groups[i], null);
                        }
                    }
                    break;
                }
            }

            this.Url = req;
        }

        protected virtual void AssertPermission(params string[] flags)
        {
            if (!this.UserHasPermission(flags))
            {
                Security.User.RedirectToLogin();
            }
        }

        protected bool UserHasPermission(string[] flags)
        {
            string[] groups = Roles.GetRolesForUser();
            foreach (string flag in flags)
            {
                if (this.Controller.Permissions.HasFlag(flag, groups))
                {
                    return true;
                }
            }
            return false;
        }
    }
}