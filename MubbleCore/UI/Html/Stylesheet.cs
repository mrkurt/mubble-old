using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Html
{
    public class Stylesheet : Control
    {

        private string href;

        public string Href
        {
            get { return href; }
            set { href = value; }
        }

        private IncludedFileType fileType = IncludedFileType.Static;

        public IncludedFileType FileType
        {
            get { return fileType; }
            set { fileType = value; }
        }

        private bool useStaticFileHost = true;

        public bool UseStaticFileHost
        {
            get { return useStaticFileHost; }
            set { useStaticFileHost = value; }
        }


        private IncludedFileLocation fileLocation = IncludedFileLocation.Template;

        public IncludedFileLocation FileLocation
        {
            get { return fileLocation; }
            set { fileLocation = value; }
        }

        private string rel = "stylesheet";

        public string Rel
        {
            get { return rel; }
            set { rel = value; }
        }

        private string media;

        public string Media
        {
            get { return media; }
            set { media = value; }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (this.FileLocation == IncludedFileLocation.Shared)
            {
                this.Href = System.IO.Path.Combine("~/Templates/shared", this.Href);
            }
            else if (this.FileLocation == IncludedFileLocation.Template)
            {
                this.Href = System.IO.Path.Combine(Controller.Template.Path, this.Href);
            }
            this.Href = Page.ResolveUrl(this.Href);
            if (this.FileType == IncludedFileType.Static)
            {
                this.Href = Config.Caching.GetCachedVersionUrl(this.Href, this.UseStaticFileHost);
            }

            int spaces = writer.Indent;
            writer.WriteBeginTag("link");

            writer.WriteAttribute("type", "text/css");

            if (this.Rel != null)
            {
                writer.WriteAttribute("rel", this.Rel);
            }

            if (this.Media != null)
            {
                writer.WriteAttribute("media", this.Media);
            }

            if (this.Href != null)
            {
                writer.WriteAttribute("href", this.Href);
            }

            writer.Write(HtmlTextWriter.SelfClosingTagEnd);
            writer.WriteLine();
            writer.Indent = spaces;
        }

    }
}
