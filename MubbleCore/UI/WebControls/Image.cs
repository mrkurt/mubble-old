using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.ComponentModel;

namespace Mubble.UI.WebControls
{
    [ToolboxData("<{0}:Image runat=server />")]
    [ToolboxItem(true)]
    public class Image : System.Web.UI.HtmlControls.HtmlImage
    {
        private string controllerPath;

        public string ControllerPath
        {
            get
            {
                if (controllerPath == null)
                {
                    ILinkable link = Control.GetCurrentScope<ILinkable>(this);
                    controllerPath = link.Url.Path;
                }
                return controllerPath;
            }
            set { controllerPath = value; }
        }

        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        /// Gets the parsed URL associated with this request
        /// </summary>
        public MubbleUrl Url
        {
            get
            {
                return ((Mubble.UI.Page)Page).Url;
            }
        }

        private Mubble.UI.Page page;

        /// <summary>
        /// Gets the container page for this control
        /// </summary>
        new public Mubble.UI.Page Page
        {
            get
            {
                if (this.page == null)
                {
                    this.page = base.Page as Mubble.UI.Page;
                }
                return page;
            }
        }

        private string pathPattern;

        public string PathPattern
        {
            get { return pathPattern; }
            set { pathPattern = value; }
        }

        private string alternateSrc;

        public string AlternateSrc
        {
            get { return alternateSrc; }
            set { alternateSrc = value; }
        }


        private Controller content;

        /// <summary>
        /// Gets the content for the current request
        /// </summary>
        public Controller Content
        {
            get
            {
                if (content == null)
                {
                    content = (this.Page != null) ?
                        this.Page.Controller : null;
                }
                return content;
            }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (this.PathPattern != null && this.Src != null && this.AlternateSrc != null)
            {
                if (Regex.IsMatch(this.Page.Controller.Path, this.PathPattern, RegexOptions.IgnoreCase))
                {
                    this.Src = this.AlternateSrc;
                }
            }
            if (!string.IsNullOrEmpty(this.FileName))
            {
                this.FileName = Control.GetReferencedValue(this, this.FileName);
            }
            if (this.FileName == null && (this.Src == null || this.Src.Length == 0))
            {
                ITitleImage ti = Control.GetCurrentScope<ITitleImage>(this);

                if (ti != null)
                {
                    this.FileName = ti.TitleImage;
                    this.ControllerPath = ti.Url.Path;

                    this.Alt = (this.Alt == null) ? ti.Url.Title : this.Alt;
                }
            }
            if (this.FileName != null)
            {
                string pathExtra = "/" + this.FileName;
                if (this.Width > 0)
                {
                    pathExtra = string.Format("/w{{{0}}}", this.Width) + pathExtra;
                    this.Width = -1;
                }
                if (this.Height > 0)
                {
                    pathExtra = string.Format("/h{{{0}}}", this.Height) + pathExtra;
                    this.Height = -1;
                }
                this.Src = MubbleUrl.Url(this.ControllerPath, pathExtra, "MediaHandler");
            }
            else if (!string.IsNullOrEmpty(this.Src) && this.Src[0] != '/' && this.Src.IndexOf("://") < 0)
            {
                Uri uri = new Uri(this.Src, UriKind.Relative);
                string newUrl = System.IO.Path.Combine(this.Page.TemplateBase, this.Src);
                this.Src = Page.ResolveUrl(newUrl);
            }
            base.Render(writer);
        }
    }
}
