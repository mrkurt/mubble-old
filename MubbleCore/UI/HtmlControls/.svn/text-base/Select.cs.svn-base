using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;

namespace Mubble.UI.HtmlControls
{
    [ToolboxData("<{0}:Select runat=server></{0}:Select>")]
    [ToolboxItem(true)]
    public class Select : System.Web.UI.HtmlControls.HtmlSelect
    {
        public string FieldName
        {
            get { return this.Attributes["FieldName"]; }
            set { this.Attributes["FieldName"] = value; }
        }


        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            FormField.SetValue(this, Context);
            base.Render(writer);
        }
        protected override void RenderAttributes(System.Web.UI.HtmlTextWriter writer)
        {
            HtmlTextWriter wrtr = FormField.PrepareWriter(this, writer);
            base.RenderAttributes(wrtr);
            wrtr.UseFieldName = false;
        }
    }
}
