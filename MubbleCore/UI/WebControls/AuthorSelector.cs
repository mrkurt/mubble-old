using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

namespace Mubble.UI.WebControls
{
    [ToolboxData("<{0}:AuthorSelector runat=server />")]
    [ToolboxItem(true)]
    public class AuthorSelector : Mubble.UI.HtmlControls.Select
    {
        public override void DataBind()
        {
            this.DataSource = Author.Find().Sort("DisplayName");
            this.DataTextField = "DisplayName";
            this.DataValueField = "UserName";
            base.DataBind();
            this.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- All -- ", ""));
            //if (this.Attributes["FieldName"] != null && Context.Request[this.Attributes["FieldName"]] != null)
            //{
            //    ListItem item = this.Items.FindByValue(Context.Request[this.Attributes["FieldName"]]);
            //    if(item != null) item.Selected = true;
            //}
        }
    }
}
