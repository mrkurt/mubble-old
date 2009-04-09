using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Html
{
    public class Javascript : Control
    {
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

        private string src;

        public string Src
        {
            get { return src; }
            set { src = value; }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (this.FileType == IncludedFileType.Application)
            {
                this.Src = string.Concat("~/application.", Handlers.Javascript.Version, ".js.ashx");
                this.Src = Page.ResolveUrl(this.Src);
            }
            else
            {
                if (this.FileLocation == IncludedFileLocation.Shared)
                {
                    this.Src = System.IO.Path.Combine("~/Templates/shared", this.Src);
                }
                else if(this.FileLocation == IncludedFileLocation.Template)
                {
                    this.Src = System.IO.Path.Combine(Controller.Template.Path, this.Src);
                }
                this.Src = Page.ResolveUrl(this.Src);
                if (this.FileType == IncludedFileType.Static)
                {
                    this.Src = Config.Caching.GetCachedVersionUrl(this.Src, this.UseStaticFileHost);
                }
            }

            int spaces = writer.Indent;
            writer.WriteBeginTag("script");
            writer.WriteAttribute("type", "text/javascript");
            writer.WriteAttribute("src", this.Src);
            writer.Write(HtmlTextWriter.TagRightChar);
            writer.WriteEndTag("script");
            writer.WriteLine();
            writer.Indent = spaces;

        }
    }

    public enum IncludedFileType
    {
        Static = 0,
        Dynamic = 1,
        Application = 2
    }

    public enum IncludedFileLocation
    {
        Template = 0,
        Shared = 1
    }
}
