using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Html
{
    public class Form : ContainerControl
    {
        private string path;

        public string Path
        {
            get 
            {
                if (path == null)
                {
                    path = this.Content.Path;
                }
                return path; 
            }
            set { path = value; }
        }

        private string handler = "HtmlHandler";

        public string Handler
        {
            get { return handler; }
            set { handler = value; }
        }


        private string method = "GET";

        public string Method
        {
            get { return method; }
            set { method = value; }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            writer.WriteBeginTag("form");
            string url = Mubble.MubbleUrl.Url(this.Path, this.Handler);
            writer.WriteAttribute("mubble-action", url);
            writer.WriteAttribute("method", this.Method);
            writer.Write(HtmlTextWriter.TagRightChar);
            writer.Write(Environment.NewLine);
            writer.Indent = writer.Indent + 1;
            this.RenderChildren(writer);
            writer.Indent = writer.Indent - 1;
            writer.WriteEndTag("form");
            
        }
    }
}
