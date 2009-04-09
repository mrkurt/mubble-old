using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;

namespace Mubble.UI.WebControls
{
    /// <summary>
    /// 
    /// </summary>
    public class HyperLink : System.Web.UI.WebControls.HyperLink
    {

        private string queryString;

        /// <summary>
        /// Gets or sets the query string for the linked URL
        /// </summary>
        public virtual string QueryString
        {
            get { return queryString; }
            set { queryString = value; }
        }

        private string handler = "HtmlHandler";

        /// <summary>
        /// The handler type to link to
        /// </summary>
        public virtual string Handler
        {
            get { return handler; }
            set { handler = value; }
        }

        private Link urlPair;

        public virtual Link UrlPair
        {
            get { return urlPair; }
            set { urlPair = value; }
        }

        private string contentPath;

        /// <summary>
        /// The path to the CMS content this should link to
        /// </summary>
        public virtual string ContentPath
        {
            get { return contentPath; }
            set { contentPath = value; }
        }

        private string contentPathExtra;

        /// <summary>
        /// Additional path information (comes after the extension) to be included in this link
        /// </summary>
        public virtual string ContentPathExtra
        {
            get { return contentPathExtra; }
            set { contentPathExtra = value; }
        }

        /// <summary>
        /// Gets the parsed URL associated with this request
        /// </summary>
        public MubbleUrl Url
        {
            get
            {
                return (Page as Mubble.UI.Page).Url;
            }
        }

        private string anchor;

        public string Anchor
        {
            get { return anchor; }
            set { anchor = value; }
        }


        private Controller content;
        /// <summary>
        /// Gets or sets the content the URL should be built from
        /// </summary>
        public Controller Content
        {
            get 
            {
                if (content == null && Parent is System.Web.UI.WebControls.RepeaterItem)
                {
                    content = ((System.Web.UI.WebControls.RepeaterItem)Parent).DataItem as Controller;
                }
                if (content == null && Page is Mubble.UI.Page)
                {
                    content = ((Mubble.UI.Page)Page).Controller;
                }
                return content; 
            }
            set { content = value; }
        }

        private bool doLink = true;
        protected virtual bool DoLink
        {
            get { return doLink; }
            set { doLink = value; }
        }
	

        public override string Text
        {
            get
            {
                if (base.Text == null && this.Content != null)
                {
                    return this.Content.Title;
                }
                return base.Text;
            }
            set { base.Text = value; }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (DoLink)
            {
                ILinkable scope = Control.GetCurrentScope<ILinkable>(this);
                Link scoped = (scope != null) ? scope.Url : null;
                string fullPath = null;
                if (this.UrlPair != null)
                {
                    this.ContentPath = this.UrlPair.Path;
                    this.ContentPathExtra = this.UrlPair.Extra;
                }
                if (this.ContentPath != null)
                {
                    fullPath = MubbleUrl.Url(this.ContentPath, this.ContentPathExtra, this.Handler);
                }
                else if (scoped != null)
                {
                    fullPath = MubbleUrl.Url(scoped.Path, (this.ContentPathExtra != null) ? this.ContentPathExtra : scoped.Extra, this.Handler);
                }
                else if (this.Content != null)
                {
                    fullPath = MubbleUrl.Url(this.Content.Path, this.ContentPathExtra, this.Handler);
                }
                if (fullPath != null)
                {
                    this.NavigateUrl = fullPath;
                }
                if ((this.Text == null || this.Text.Trim().Length == 0) && this.Controls.Count == 0 && scoped != null)
                {
                    this.Text = scoped.Title;
                }
                if (this.NavigateUrl != null && this.QueryString != null)
                {
                    this.NavigateUrl += (this.QueryString.IndexOf('?') >= 0) ? "" : "?";
                    this.NavigateUrl += this.QueryString;
                }
                if (this.NavigateUrl != null && this.Anchor != null && this.Anchor.Length > 0)
                {
                    this.NavigateUrl += "#" + this.Anchor;
                }
            }
            else
            {
                this.NavigateUrl = null;
            }
            base.Render(writer);
        }
    }
}
