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
using Mubble.Models;
using System.Collections.Generic;

public partial class Admin_UserControls_Metadata_StringValue : Mubble.UI.Admin.MetaDataEditor
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtTitle.InnerHtml = this.Title;
        this.txtDescription.InnerHtml = this.Description;

        if (this.MetaData == null) return;
        MetaDataCollection metadata = this.MetaData;

        if (!Page.IsPostBack && metadata != null)
        {
            this.txtValue.Text = metadata.GetFirstStringValue(this.Field);
        }
        else if (Page.IsPostBack && metadata != null)
        {
            metadata.Clear(this.Field);
            metadata.Set(this.Field, this.txtValue.Text);
        }
    }
}
