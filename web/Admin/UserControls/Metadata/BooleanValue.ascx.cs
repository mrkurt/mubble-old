using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Mubble.Models;

public partial class Admin_UserControls_Metadata_BooleanValue : Mubble.UI.Admin.MetaDataEditor
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtTitle.InnerHtml = this.Title;
        this.chckTrue.Text = this.Description;

        if (this.MetaData == null) return;
        MetaDataCollection metadata = this.MetaData;

        if (!Page.IsPostBack)
        {
            if (metadata.GetFirstStringValue(this.Field) != null)
            {
                this.chckTrue.Checked = true;
            }
        }
        else if(Page.IsPostBack)
        {
            metadata.Clear(this.Field);

            if (this.chckTrue.Checked) metadata.Set(this.Field, "true");
        }
    }
}
