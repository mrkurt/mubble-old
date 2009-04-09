using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using System.Web.UI;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Mubble.UI.Html
{
    [ToolboxData("<{0}:Image runat=server />")]
    [ToolboxItem(true)]
    public class Image : System.Web.UI.HtmlControls.HtmlImage, IControl
    {
        private string alternateSrc;

        public string AlternateSrc
        {
            get { return alternateSrc; }
            set { alternateSrc = value; }
        }

        private string pathPattern;

        public string PathPattern
        {
            get { return pathPattern; }
            set { pathPattern = value; }
        }

        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        private bool useStaticCache = true;

        public bool UseStaticCache
        {
            get { return useStaticCache; }
            set { useStaticCache = value; }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.FileName))
            {
                this.FileName = Control.GetReferencedValue(this, this.FileName);
                string pathExtra = string.Concat("/", this.FileName);
                if (this.Height > 0)
                {
                    pathExtra = string.Concat("/", this.Height, pathExtra);
                }
                if (this.Width > 0)
                {
                    pathExtra = string.Concat("/", this.Width, pathExtra);
                }

                if (string.IsNullOrEmpty(this.Path))
                {
                    ILinkable link = Control.GetCurrentScope<ILinkable>(this);
                    if (link != null)
                    {
                        this.Path = link.Url.Path;
                    }
                    if (string.IsNullOrEmpty(this.Alt))
                    {
                        this.Alt = link.Url.Title;
                    }
                }
                this.Src = MubbleUrl.Url(this.Path, pathExtra, "MediaHandler");
            }
            if (!string.IsNullOrEmpty(this.AlternateSrc) && !string.IsNullOrEmpty(this.PathPattern))
            {
                if (Regex.IsMatch(this.Url.Path, this.PathPattern))
                {
                    this.Src = this.AlternateSrc;
                }
            }
            if (!string.IsNullOrEmpty(this.Src) && !this.Src.StartsWith("http"))
            {
                string url = this.Src;
                if (!url.StartsWith("/") && !url.StartsWith("~/"))
                {
                    url = System.IO.Path.Combine(this.Content.Template.Path, url);
                }
                url = Page.ResolveUrl(url);

                this.Src = url;
            }
            if (this.UseStaticCache)
            {
                this.Src = Config.Caching.GetCachedVersionUrl(this.Src);
            }
            base.Render(writer);
        }

        #region IControl Members

        public PathParameters Params
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        private Page page;
        public new Page Page
        {
            get { if (page == null) page = base.Page as Page; return page; }
        }

        private Controller content;
        /// <summary>
        /// Gets or sets the content the URL should be built from
        /// </summary>
        public Controller Content
        {
            get
            {
                if (content == null)
                {
                    content = Control.GetCurrentScope<Controller>(this);
                    if (content == null)
                    {
                        content = this.Page.Controller;
                    }
                }
                return content;
            }
            set { content = value; }
        }

        /// <summary>
        /// Gets the parsed URL associated with this request
        /// </summary>
        public MubbleUrl Url
        {
            get
            {
                return this.Page.Url;
            }
        }

        #endregion
    }
}
