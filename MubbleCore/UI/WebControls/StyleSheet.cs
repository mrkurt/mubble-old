using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Mubble.UI.WebControls
{
    [ParseChildren(false)]
    [ToolboxData("<{0}:StyleSheet runat=server />")]
    public class StyleSheet : System.Web.UI.Control
    {
        private string href;
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Href
        {
            get { return href; }
            set { href = value; }
        }

        public StyleSheet()
        {
            this.PreRender += new EventHandler(StyleSheet_PreRender);
        }

        void StyleSheet_PreRender(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                HtmlLink link = new HtmlLink();
                link.Attributes.Add("type", "text/css");
                link.Attributes.Add("rel", "stylesheet");
                link.Attributes.Add("href", this.Href);
                Page.Header.Controls.Add(link);
            }
        }
    }
}
