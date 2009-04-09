using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Mubble.Web.Admin
{
    public partial class SectionPicker : Mubble.UI.UserControl
    {
        public event EventHandler SectionClick;
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void SectionButton_Click(object sender, EventArgs e)
        {
            //HACK: This should really be a more intelligent event, rather than just sending the link button off
            if (this.SectionClick != null)
            {
                this.SectionClick(sender, e);
            }
        }
    }
}
