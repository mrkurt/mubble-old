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
using System.Net.Mail;

namespace Arstechnica.Custom
{
    public partial class Contact : Mubble.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["type"]) && this.Destination.Items.FindByValue(Request.QueryString["type"]) != null)
                {
                    foreach (ListItem item in this.Destination.Items)
                    {
                        if (item.Value.Equals(Request.QueryString["type"], StringComparison.CurrentCultureIgnoreCase))
                        {
                            item.Selected = true;
                            break;
                        }
                        else
                        {
                            item.Selected = false;
                        }
                    }
                }
                this.Url.Text = Request.QueryString["url"];
            }
        }

        protected void SendMessage_Click(object sender, System.EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
					string email = this.Destination.Text;
					string subject = "Ars FB: {0}";
					if ("comments".Equals(email))
					{
						email = "editors";
					}
					if("press".Equals(email)){
						email = "ken";
						subject = "Press: {0}";
					}
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(
                        new MailAddress(this.Email.Text, this.Name.Text),
                        new MailAddress(email + "@arstechnica.com")
                    );
                    msg.Subject = string.Format(subject, this.Subject.Text);
                    msg.Body = "A site visitor just submitted feedback using the Ars contact form.";
                    if (!string.IsNullOrEmpty(this.Url.Text))
                    {
                        msg.Body += System.Environment.NewLine + System.Environment.NewLine + "Referenced URL: " + this.Url.Text;
                    }
                    msg.Body += "\r\n\r\n----------\r\n\r\n" + this.Message.Text;

                    SmtpClient mailer = new SmtpClient();
                    mailer.Send(msg);
                }
                catch (FormatException) { }

                this.ContactForm.Visible = false;
                this.Confirmation.Visible = true;
            }
            else
            {
                this.ErrorMessage.Visible = true;
            }
        }
    }
}
