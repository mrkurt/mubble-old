using System;
using System.Collections.Generic;
using System.Text;
using Mubble.UI.Data;
using Mubble.Models;

namespace Mubble.UI.Html
{
    public class PagingDropDownList : Mubble.UI.Control
    {
        private string pageLinkFor;

        public string PageLinkFor
        {
            get { return pageLinkFor; }
            set { pageLinkFor = value; }
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

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (this.PageLinkFor != null)
            {
                IPaging paging = this.NamingContainer.FindControl(this.PageLinkFor) as IPaging;
                if (paging == null)
                {
                    throw new ArgumentException(
                        string.Format(
                        "The control specified using PageLinkFor ({0}) is not valid.  This property must be set to the ID of "
                        + "a control that implements IPaging.",
                        this.PageLinkFor
                        )
                    );
                }

                writer.WriteBeginTag("select");
                writer.WriteAttribute("onchange", "location = this.options[this.selectedIndex].value;");
                writer.Write(HtmlTextWriter.TagRightChar);

                if (paging.PageLinks != null)
                {
                    foreach (Link pair in paging.PageLinks)
                    {
                        writer.WriteBeginTag("option");
                        writer.WriteAttribute("value", MubbleUrl.Url(pair, this.Handler));
                        if (pair.Equals(paging.CurrentPageLink)) writer.WriteAttribute("selected", "selected");
                        writer.Write(HtmlTextWriter.TagRightChar);
                        writer.Write(pair.Title);
                        writer.WriteEndTag("option");
                    }
                }
                writer.WriteEndTag("select");

                base.Render(writer);
            }
        }
    }
}
