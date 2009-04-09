using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using System.Net.Mail;

using Newtonsoft.Json;

namespace Mubble.Handlers
{
    public class Mailer : HttpHandler
    {
        private MailerMode mode = MailerMode.User;

        public override void ProcessMubbleRequest(System.Web.HttpContext context)
        {
            MailerResponse response = new MailerResponse();

            response.Type = MailerResponseType.InvalidFromAddress;
            response.Message = "This feature is currently disabled.";
            this.sendResponse(response, context);
            return;

            IRssItem item = Controller.GetCurrentRssItem(this.Url.Path, this.Url.PathExtra, Security.User.GetRoles());

            if (item == null)
            {
                response.Type = MailerResponseType.NoContentItem;
                response.Message = "There is no content available at that URL.";

                this.sendResponse(response, context);
                return;
            }

            string toAddresses = context.Request.Form["RecipientEmail"];
            string[] to = new string[0];
            if (toAddresses != null)
            {
                to = toAddresses.Split(new char[] { ',', ' ', ';' });
            }

            if (to.Length > 5)
            {
                response.Type = MailerResponseType.TooManyRecipients;
                response.Message = "Only 5 recipients allowed.";
                this.sendResponse(response, context);
                return;
            }

            MailAddress fromAddress = null;
            try
            {
                fromAddress = new MailAddress(context.Request.Form["SenderEmail"], context.Request.Form["SenderName"]);
            }
            catch (Exception e)
            {
                response.Type = MailerResponseType.InvalidFromAddress;
                response.Message = "The from address is not valid.";
                this.sendResponse(response, context);
                return;
            }

            string subject = string.Format(
                "{0} wants you to read \"{1}\"", 
                fromAddress.ToString(), 
                item.Title
                );
            string message = string.Format(
                "{0}\r\n\r\n--------------\r\n\r\n{1}\r\n\r\n--------------\r\n\r\n{2}", 
                context.Request.Form["Message"],
                MubbleUrl.Url(item.Url, "HtmlHandler"),
                item.Excerpt
                );

            foreach (string address in to)
            {
                MailAddress toAddress = null;
                try
                {
                    toAddress = new MailAddress(address);
                }
                catch (Exception e)
                {
                    response.Type = MailerResponseType.InvalidRecipientAddress;
                    response.Message = string.Format("\"{0}\" is not a valid email address", address);
                }
                SentEmail sent = new SentEmail();
                sent.SenderEmail = fromAddress.Address;
                sent.SenderIP = context.Request.UserHostAddress;
                sent.SenderName = fromAddress.DisplayName;
                sent.RecipientEmail = address;
                sent.SentAt = DateTime.Now;
                sent.IsSpam = false;

                if (sent.IsSpammer)
                {
                    response.Type = MailerResponseType.Spam;
                    response.Message = "The system thinks this is spam.";
                    this.sendResponse(response, context);
                    return;
                }

                sent.Save();

                MailMessage msg = new MailMessage(fromAddress, toAddress);
                msg.Subject = subject;
                msg.Body = message;
                msg.IsBodyHtml = false;

                SmtpClient mailer = new SmtpClient();
                mailer.Send(msg);
            }

            response.Type = MailerResponseType.Success;
            this.sendResponse(response, context);
        }

        private void sendResponse(MailerResponse response, System.Web.HttpContext context)
        {
            context.Response.Write(JavaScriptConvert.SerializeObject(response));
        }

        private void getMessage()
        {

        }

        private void getSubject()
        {

        }
    }
    enum MailerMode
    {
        User = 0,
        Staff = 1
    }
}
