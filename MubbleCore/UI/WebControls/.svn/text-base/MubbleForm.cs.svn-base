using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Web.UI;

namespace Mubble.UI.WebControls
{
    /// <summary>
    /// A superset of HtmlForm that will allow a specific post back URL
    /// </summary>
    /*[DefaultProperty("Text"),
    ToolboxData("<{0}:form runat=server></{0}:form>")]*/
    public class Form : HtmlForm
    {
        public bool MakeCompliant = true;

        /// <summary> 
        /// Render this control's attributes to the output parameter specified.
        /// </summary>
        /// <param name="output"> The HTML writer to write out to </param>
        protected override void RenderAttributes(HtmlTextWriter output)
        {
            output.WriteAttribute("action", Context.Request.RawUrl, true);
            this.Attributes.Remove("action");
            output.WriteAttribute("method", base.Method);
            this.Attributes.Remove("method");
            this.Attributes.Remove("id");

            if (!this.MakeCompliant)
            {
                output.WriteAttribute("name", base.Name);
                this.Attributes.Remove("name");
            }

            output.WriteAttribute("id", this.ClientID);
            this.Attributes.Remove("id");
        
            if (base.Enctype != "")
            {
                output.WriteAttribute("enctype", base.Enctype);
                this.Attributes.Remove("enctype");
            }

            output.WriteAttribute("onsubmit", "javascript:return WebForm_OnSubmit();");

            this.Attributes.Remove("language");
            this.Attributes.Remove("onsumbit");

            foreach (string key in this.Attributes.Keys)
            {
                output.WriteAttribute(key, this.Attributes[key]);
            }
        }
    }
}
