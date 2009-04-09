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

namespace Mubble.Web.Admin
{

    public partial class Query : Mubble.UI.AdminPage
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                LinkButton button = sender as LinkButton;
                if (button.CommandArgument == "Draft")
                {
                    Controller.Status = PublishStatus.Draft;
                }
                if (button.CommandArgument == "Publish")
                {
                    Controller.Status = PublishStatus.Published;
                }
                Controller.Title = this.txtTitle.Text;
                Controller.Body = this.txtBody.Text;
                if (Controller.PublishDate == DateTime.MinValue)
                {
                    Controller.PublishDate = DateTime.Now;
                }

                Controller.MetaData.Clear("Query");
                Controller.MetaData.Set("Query", this.txtQuery.Text, false);

                Controller.IsContentContainer = this.chckAllowPublish.Checked;

                Controller.Save();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.AssertPermission("full");
            if (!Page.IsPostBack)
            {
                this.txtQuery.Text = Controller.MetaData.GetFirstStringValue("Query");
                this.txtTitle.Text = Controller.Title;
                this.txtBody.Text = Controller.Body;
                this.chckAllowPublish.Checked = Controller.IsContentContainer;
            }
        }

    }
}