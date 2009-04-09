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

public partial class Templates_ars_Tags: System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected override void Render(HtmlTextWriter writer)
    {
        this.Tags.DataBind();
        if (this.Tags.Items.Count > 0)
        {
            base.Render(writer);
        }
    }
}
