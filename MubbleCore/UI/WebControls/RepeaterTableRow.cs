using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Mubble.UI.WebControls
{
    public class RepeaterTableRow : System.Web.UI.HtmlControls.HtmlTableRow
    {
        private string alternatingCssClass;

        public string AlternatingCssClass
        {
            get { return alternatingCssClass; }
            set { alternatingCssClass = value; }
        }

        private RepeaterItem parentRepeaterItem;
        protected int RowNumber
        {
            get
            {
                if (parentRepeaterItem == null)
                {
                    System.Web.UI.Control c = this.NamingContainer;

                    do
                    {
                        RepeaterItem r = c as RepeaterItem;
                        if (r != null)
                        {
                            parentRepeaterItem = r;
                            break;
                        }
                        c = c.NamingContainer;
                    } while (c != null);
                }
                if (parentRepeaterItem == null)
                {
                    return -1;
                }
                else
                {
                    return parentRepeaterItem.ItemIndex;
                }
            }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (this.RowNumber > -1 && this.RowNumber % 2 != 0)
            {
                this.Attributes["Class"] = this.AlternatingCssClass;
            }
            base.Render(writer);
        }
    }
}
