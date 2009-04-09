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
using System.Net;

namespace Arstechnica.Custom
{
    public partial class Login : Mubble.UI.Page
    {
        string ForumAuthUrl = "http://episteme.arstechnica.com/eve?a=groups&LOGIN_USERNAME={0}&LOGIN_PASSWORD={1}&xsl=custom_1";
        string groupId = "|4730922912|";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.LoginForm.Visible = !User.Identity.IsAuthenticated;
            this.LoginForm.DestinationPageUrl = Request.QueryString["origin"];
        }

        protected void UserAuthenticate(object sender, AuthenticateEventArgs e)
        {
            if (Membership.ValidateUser(this.LoginForm.UserName, this.LoginForm.Password))
            {
                e.Authenticated = true;
                return;
            }

            string url = string.Format(
                this.ForumAuthUrl,
                HttpUtility.UrlEncode(this.LoginForm.UserName),
                HttpUtility.UrlEncode(this.LoginForm.Password)
                );

            WebClient web = new WebClient();
            string response = web.DownloadString(url);

            if (response.Contains(groupId))
            {
                e.Authenticated = Membership.ValidateUser("Premier Subscriber", "danu2HEt");
                this.LoginForm.UserName = "Premier Subscriber";

                HttpCookie cookie = new HttpCookie("ForumUsername", this.LoginForm.UserName);
                cookie.Expires = DateTime.Now.AddMonths(2);

                Response.Cookies.Add(cookie);
            }
        }

        /*  
            USERNAME=Kurt
            REGISTRATION_DATE=1999-06-01
            EMAIL=kurt@arstechnica.com
            IS_EMAIL_VERIFIED=Y
            IS_BANNED=N
            USER_OID=81509982
            SECURITY_GROUPS=|32009868|80009562|99609816|1920904073|4900903374|6330927813|8860982374|
            USER_TYPE_DESCRIPTION=
        */
    }
}
