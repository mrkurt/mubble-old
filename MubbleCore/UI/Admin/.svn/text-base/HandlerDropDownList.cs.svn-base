using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Admin
{
    public class HandlerDropDownList : System.Web.UI.WebControls.DropDownList
    {
        public override void DataBind()
        {
            this.DataSource = Mubble.Handlers.Settings.Extensions;
            this.DataTextField = "Value";
            this.DataValueField = "Key";
            base.DataBind();

            this.SelectedValue = "Mubble.Handlers.HtmlHandler";
        }
    }
}
