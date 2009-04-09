using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Html
{
    public class Script : System.Web.UI.Control
    {
        private string src;

        public string Src
        {
            get { return src; }
            set { src = value; }
        }

        private string type = "text/javascript";

        public string Type
        {
            get { return type; }
            set { type = value; }
        }


        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            writer.WriteLine();
            writer.WriteBeginTag("script");

            if (this.Type != null)
            {
                writer.WriteAttribute("type", this.Type);
            }

            if (this.Src != null)
            {
                writer.WriteAttribute("src", Control.ResolveUrl(this, this.Src));
            }

            writer.Write(HtmlTextWriter.TagRightChar);
            writer.WriteEndTag("script");
            base.Render(writer);
        }
    }
}
