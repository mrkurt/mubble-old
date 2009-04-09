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
using System.IO;
using System.Web.Configuration;
using System.Drawing;
using System.Xml;
using System.Collections.Generic;

namespace Mubble.Web.Admin
{
    public partial class Media : Mubble.UI.AdminPage
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.AssertPermission("full", "publish", "draft");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Url.GetPathItem(1, "browse").ToLower())
            {
                case "delete":
                    this.DeleteMedia(new Guid(Url.GetPathItem(2, Guid.Empty.ToString())));
                    this.RedirectToSource();
                    break;
            }
        }

        protected void RedirectToSource()
        {
            string source = Request["source"];
            if (source == null || source == "")
            {
                source = Url.ToString("AdminHandler", Url.PathExtra);
            }
            Response.Redirect(source);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (file.HasFile)
            {
                Mubble.Models.File m = new Mubble.Models.File();
                m.ControllerID = this.Controller.ID;
                m.FileName = file.FileName;
                m.Name = file.FileName;
                m.UpdatedAt = DateTime.Now;

                m.Save(file.FileContent);

                Response.Redirect(Url.ToString("AdminHandler"));
            }
        }

        protected void DeleteMedia(Guid id)
        {
            Mubble.Models.File m = new Mubble.Models.File();
            if (m.Load(id))
            {
                m.Delete();
            }
        }
    }
}
