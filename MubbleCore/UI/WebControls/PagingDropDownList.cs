using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.WebControls
{
    public class PagingDropDownList : System.Web.UI.Control
    {
        private string pageLinkFor;

        public string PageLinkFor
        {
            get { return pageLinkFor; }
            set { pageLinkFor = value; }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {

            IPageable paging = this.NamingContainer.FindControl(this.PageLinkFor) as IPageable;
            if (paging == null)
            {
                throw new ArgumentException(
                    string.Format(
                    "The control specified using PageLinkFor ({0}) is not valid.  This property must be set to the ID of "
                    + "a control that implements IPageable.",
                    this.PageLinkFor
                    )
                );
            }

            writer.WriteBeginTag("select");
            writer.WriteAttribute("onchange", "location = this.options[this.selectedIndex].value;");
            writer.Write(HtmlTextWriter.TagRightChar);

            foreach (PagePair pair in paging.AllPages)
            {
                writer.WriteBeginTag("option");
                writer.WriteAttribute("value", pair.Link);
                if (pair.Link == paging.CurrentPage.Link) writer.WriteAttribute("selected", "selected");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Write(pair.Name);
                writer.WriteEndTag("option");
            }
            writer.WriteEndTag("select");
            base.Render(writer);
        }
    }
}
