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

    public partial class Content : Mubble.UI.AdminPage
    {
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                LinkButton button = sender as LinkButton;
                if (button.CommandArgument == "Draft")
                {
                    Response.Write("Drafty!");
                    Controller.Status = Mubble.Models.PublishStatus.Draft;
                }
                if (button.CommandArgument == "Publish")
                {
                    Response.Write("Live!");
                    Controller.Status = Mubble.Models.PublishStatus.Published;
                }
                Controller.Title = this.txtTitle.Text;
                Controller.Body = this.txtBody.Text;
                Controller.PublishDate = DateTime.Parse(
                        string.Format(
                            "{0}/{1}/{2} {3}:{4} {5}",
                            this.lstMonth.SelectedValue,
                            this.lstDay.SelectedValue,
                            this.lstYear.SelectedValue,
                            this.lstHour.SelectedValue,
                            this.lstMinute.SelectedValue,
                            this.lstAmPm.SelectedValue
                        )
                    );
                Controller.Save();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.AssertPermission("full", "publish", "draft");
            if (!Page.IsPostBack)
            {
                this.txtTitle.Text = Controller.Title;
                this.txtBody.Text = Controller.Body;
                #region Initialize date crapola
                this.lstMonth.SelectedValue = Controller.PublishDate.Month.ToString();
                for (int i = 1; i <= 31; i++)
                {
                    this.lstDay.Items.Add(i.ToString());
                }
                this.lstDay.SelectedValue = Controller.PublishDate.Day.ToString();

                for (int i = 1995; i <= DateTime.Now.Year + 10; i++)
                {
                    this.lstYear.Items.Add(i.ToString());
                }
                this.lstYear.SelectedValue = Controller.PublishDate.Year.ToString();

                for (int i = 1; i <= 12; i++)
                {
                    ListItem item = new ListItem();
                    item.Value = i.ToString();
                    item.Text = (i < 10) ? "0" + i.ToString() : i.ToString();
                    item.Selected = (i == Controller.PublishDate.Hour || i + 12 == Controller.PublishDate.Hour);

                    this.lstHour.Items.Add(item);
                }

                for (int i = 0; i <= 59; i++)
                {
                    ListItem item = new ListItem();
                    item.Value = i.ToString();
                    item.Text = (i < 10) ? "0" + i.ToString() : i.ToString();
                    item.Selected = (i == Controller.PublishDate.Minute);

                    this.lstMinute.Items.Add(item);
                }

                this.lstAmPm.SelectedValue = (Controller.PublishDate.Hour >= 12) ? "PM" : "AM";
                #endregion
            }
        }
    }
}