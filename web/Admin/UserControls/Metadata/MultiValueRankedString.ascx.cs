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

public partial class Admin_UserControls_Metadata_MultiValueRankedString : Mubble.UI.Admin.MetaDataEditor
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtTitle.InnerHtml = this.Title;
        this.txtDescription.InnerHtml = this.Description;

        if (this.MetaData == null) return;
        MetaDataCollection metadata = this.MetaData;

        if (!Page.IsPostBack && metadata != null)
        {
            foreach (KeyValuePair<string,string> value in metadata[this.Field])
            {
                if (this.txtValues.Text.Length > 0)
                {
                    this.txtValues.Text += Environment.NewLine;
                }
                this.txtValues.Text += value.Value;
            }
        }
        else if (Page.IsPostBack && metadata != null)
        {
            string[] values = this.txtValues.Text.Trim().Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            metadata.Clear(this.Field);
            for (int i = 0; i < values.Length; i++)
            {
                metadata.Set(this.Field, values[i], i);
            }
        }
    }
}
