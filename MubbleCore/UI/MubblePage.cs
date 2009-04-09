using System;
using System.Collections.Generic;
using System.Text;

using Mubble;
using Mubble.UI;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI;
using System.Collections;
using Mubble.Models;
using System.Web.Security;
using System.Web.Caching;
using System.Web;
using ActiveObjects;
using System.ComponentModel;

namespace Mubble.UI
{
    #region Hackerific!
    /// <summary>
    /// This class should not be used.  It's a hack to the Mubble.UI.Page class can contain a new Page member
    /// </summary>
    public class MubblePage : System.Web.UI.Page
    {
        new public Mubble.UI.Page Page
        {
            get
            {
                return this as Mubble.UI.Page;
            }
        }
    } 
    #endregion

    /// <summary>
    /// The base class for content type pages
    /// </summary>
    public class Page : MubblePage, IScoped
    {
        #region Properties

        private bool postAllowed = true;

        public bool PostAllowed
        {
            get { return postAllowed; }
            set { postAllowed = value; }
        }


        private PathParameters _params;

        /// <summary>
        /// Gets or sets the request parameters passed through as part of the extra path
        /// </summary>
        public PathParameters Params
        {
            get { return _params; }
            set { _params = value; }
        }


        private bool isAdmin = false;

        /// <summary>
        /// Gets or sets a flag indicating whether this is an administrative page
        /// </summary>
        public bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; }
        }
	
        private MubbleUrl url;

        /// <summary>
        /// Request's parsed URL
        /// </summary>
        public MubbleUrl Url
        {
            get { return url; }
            set { url = value; }
        }

        private Controller controller;

        /// <summary>
        /// Requested content
        /// </summary>
        public Controller Controller
        {
            get { return controller; }
            set { controller = value; }
        }
        #endregion
	
        #region Url types

        private string templateBase;
        /// <summary>
        /// Gets the full URL to the base template directory
        /// </summary>
        public string TemplateBase
        {
            get 
            {
                if (this.templateBase == null)
                {
                    this.templateBase = Page.ResolveUrl(Controller.Template.Path);
                }
                return this.templateBase;
            }
        }

        /// <summary>
        /// Gets the Admin url for the current page
        /// </summary>
        public string AdminUrl
        {
            get { return this.Url.ToString("AdminHandler"); }
        }

        /// <summary>
        /// Gets the base media URL for the current page
        /// </summary>
        public string MediaUrl
        {
            get { return this.Url.ToString("MediaHandler", null); }
        }

        /// <summary>
        /// Gets the base Html url for the current page
        /// </summary>
        public string HtmlUrl
        {
            get { return this.Url.ToString("HtmlHandler"); }
        }

        public string CurrentUrl
        {
            get { return this.Url.ToString(); }
        }
        #endregion

        public Page()
            : base()
        {
            this.PreRender +=new EventHandler(MubblePage_PreRender);
            this.PreInit += new EventHandler(MubblePage_PreInit);
            this.Load += new EventHandler(MubblePage_Load);
        }
        #region Utility functions
        /// <summary>
        /// Returns the specified string if the "check" matches the "instance".
        /// </summary>
        /// <param name="check">The value to check for</param>
        /// <param name="instance">The value to check against</param>
        /// <param name="output">The value to return if the "check" and "instance" match</param>
        /// <returns>The value contained in "output" if the check succeeds, an empty string otherwise</returns>
        public string ConditionalWrite(object check, object instance , string output)
        {
            if (check.GetType() != instance.GetType())
            {
                return string.Empty;
            }
            if (check.Equals(instance))
            {
                return output;
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Gets the specified content
        /// </summary>
        /// <param name="path">Path to the content</param>
        /// <returns>The requested Content Object</returns>
        public Controller GetContent(string path)
        {
            return new Controller(path);
        }

        /// <summary>
        /// Adds a stylesheet to the page's &lt;head&gt; tag
        /// </summary>
        /// <param name="url"></param>
        public void RegisterStylesheet(string url)
        {
            HtmlLink link = new HtmlLink();
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            link.Attributes.Add("href", url);
            this.Header.Controls.Add(link);
        }
        #endregion

        #region Security Functions

        public void AssertUserPermission(params PermissionType[] types)
        {
            if (!Security.User.HasPermission(this.Controller, types))
            {
                Security.User.RedirectToLogin();
            }
        }

        #endregion

        #region Cache functions
        private List<string> cacheKeyDependencies = new List<string>();
        private List<string> fileDependencies = new List<string>();
        private List<string> queryResultDependencies = new List<string>();

        public void SetCacheDependency(IActiveObject obj)
        {
            this.SetCacheDependency(CacheBroker.GetKey(obj));
        }

        public void SetCacheDependency(string key)
        {
            if (!cacheKeyDependencies.Contains(key))
            {
                cacheKeyDependencies.Add(key);
            }
        }

        public void SetFileDependency(string path)
        {
            if (!fileDependencies.Contains(path))
            {
                fileDependencies.Add(path);
            }
        }

        public void SetQueryResultDependency(string key)
        {
            if(!queryResultDependencies.Contains(key))
            {
                queryResultDependencies.Add(key);
            }
        }

        void SetCacheDependencies()
        {            
            string[] keys = cacheKeyDependencies.ToArray();
            string[] files = fileDependencies.ToArray();
           
            if (keys.Length > 0 || files.Length > 0)
            {
                Response.AddCacheDependency(new CacheDependency(files, keys));
            }

            foreach (string key in queryResultDependencies)
            {
                Response.AddCacheDependency(new QueryResultDependency(key));
            }
        }
        #endregion

        /// <summary>
        /// Attached to the Page.PreInit event.  Sets the template using the Template property of the selected content.
        /// If the request is for an admin page, uses the default admin template.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">EventArgs for the event</param>
        protected void MubblePage_PreInit(object sender, System.EventArgs e)
        {
            //Sets the page template if this is not an admin request
            if ("post".Equals(Request.RequestType, StringComparison.CurrentCultureIgnoreCase) && !this.PostAllowed)
            {
                Response.StatusCode = 403;
                Response.Write("This page does not allow POSTs");
                Response.End();
                return;
            }
            if (!this.IsAdmin)
            {
                this.EnableViewState = false;
                this.templateBase = Page.ResolveUrl(Controller.Template.Path);
                this.MasterPageFile = System.IO.Path.Combine(Controller.Template.Path, Controller.TemplateControl);
            }
            else
            {
                this.templateBase = Page.ResolveUrl(Mubble.Config.Administration.Current.TemplatePath);
                string masterLoc = this.MasterPageFile = Mubble.Config.Administration.Current.TemplatePath
                        + Request.QueryString["template"] + ".master";
                string defaultMasterLoc = Mubble.Config.Administration.Current.TemplatePath
                        + Mubble.Config.Administration.Current.TemplateFileName;

                if (System.IO.File.Exists(Server.MapPath(masterLoc)))
                {
                    this.MasterPageFile = masterLoc;
                }
                else
                {
                    this.MasterPageFile = defaultMasterLoc;
                }
            }
        }

        /// <summary>
        /// Attached to the Page.Load event.  Verifies that content blocks exist in the Master page template.
        /// If you don't like it, override it.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">EventArgs for the event.</param>
        protected void MubblePage_Load(object sender, System.EventArgs e)
        {
            this.VerifyContentBlocks();
            if (!Config.Caching.Current.EnableOutputCaching)
            {
                Response.Cache.SetNoServerCaching();
            }
        }

        protected void VerifyContentBlocks()
        {
            //Need to verify that ContentPlaceHolder controls exist for each Content control within the page
            // Is there a better way to do this?
            foreach (System.Web.UI.Control block in this.Controls)
            {
                if (block.GetType() != typeof(System.Web.UI.WebControls.Content))
                {
                    continue;
                }
                System.Web.UI.WebControls.Content c = block as System.Web.UI.WebControls.Content;
                if (this.Master.FindControl(c.ContentPlaceHolderID) == null)
                {
                    System.Web.UI.WebControls.ContentPlaceHolder p = new System.Web.UI.WebControls.ContentPlaceHolder();
                    p.Visible = false;
                    p.ID = c.ContentPlaceHolderID;
                    this.Master.Controls.Add(p);
                }
            }
        }

        /// <summary>
        /// Attached to the Page.PreRender event.  DataBinds the page.
        /// If you don't like it, override it.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">EventArgs for the event.</param>
        protected void MubblePage_PreRender(object sender, System.EventArgs e)
        {
            //this.Title = this.Controller.Title;
            if (string.IsNullOrEmpty(this.Title)) this.Title = Controller.Title;
            this.DataBind();
        }

        /// <summary>
        /// Fixes the postback URLs for forms.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter instance to write to.</param>
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            writer = new Mubble.UI.HtmlTextWriter(writer);
            base.Render(writer);
            SetCacheDependencies();
        }

        public void SetRequestVariables(Mubble.Handlers.HttpHandler handler)
        {
            this.Controller = handler.Controller;
            this.Url = handler.Url;
            this.Params = handler.PathParts;
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
            string[] groups = Mubble.Security.User.GetRoles();
            foreach (string flag in flags)
            {
                if (this.Controller.Permissions.HasFlag(flag, groups))
                {
                    return true;
                }
            }
            return false;
        }

        public static T FindFirst<T>(System.Web.UI.Control control, string id) where T : class
        {
            T found = control.FindControl(id) as T;
            if (found == null)
            {
                foreach (System.Web.UI.Control c in control.Controls)
                {
                    found = FindFirst<T>(c, id);
                    if (found != null) return found;
                }
            }
            return found;
        }

        #region IScoped Members

        public object Scope
        {
            get { return this.Controller; }
        }

        #endregion

        public void SetPageTitle(string title)
        {
            if (!string.IsNullOrEmpty(this.Title))
            {
                this.Title = string.Format(this.Title, title);
            }
            else
            {
                this.Title = title;
            }
        }
    }
}