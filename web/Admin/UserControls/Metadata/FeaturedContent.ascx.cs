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

public partial class Admin_UserControls_Metadata_FeaturedContent : Mubble.UI.Admin.MetaDataEditor
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtTitle.InnerHtml = this.Title;
        this.txtDescription.InnerHtml = this.Description;

        if (this.Description == null) this.txtDescription.Visible = false;

        if (this.MetaData == null) return;
        MetaDataCollection metadata = this.MetaData;

        string featuredImage = metadata.GetFirstStringValue("FeaturedIcon");
        if (featuredImage != null)
        {
            this.imgFeaturedIcon.FileName = featuredImage;
        }

        if (!Page.IsPostBack)
        {
            if (metadata.GetFirstStringValue("IsFeatured") != null)
            {
                this.chckFeatured.Checked = true;
            }
        }
        else if (Page.IsPostBack)
        {
            metadata.Clear("IsFeatured");

            if (this.chckFeatured.Checked) metadata.Set("IsFeatured", "true");

            if (this.FeaturedIconUploader.HasFile)
            {
                Mubble.Models.File file = new Mubble.Models.File();
                file.ControllerID = this.Content.ID;
                file.FileName = this.FeaturedIconUploader.FileName;
                file.Name = this.FeaturedIconUploader.FileName;
                file.Save(this.FeaturedIconUploader.FileContent);

                metadata.Clear("FeaturedIcon");
                metadata.Set("FeaturedIcon", file.FileName, false);
                this.imgFeaturedIcon.FileName = file.FileName;
            }
        }

        if (this.imgFeaturedIcon.FileName != null) this.imgFeaturedIcon.Visible = true;
    }
}
