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

namespace Mubble.Web.Admin
{
    public partial class Users : Mubble.UI.AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.AssertPermission("full");
            if (!Page.IsPostBack)
            {
                //Mubble.User u = new User();
                //this.AllUsers.DataSource = u.List();

                this.AllUsers.DataSource = Membership.GetAllUsers();

                //if (Url.GetPathItem(0, "list") == "user")
                //{
                //    Mubble.User user = new User(Url.GetPathItem(1, string.Empty));
                //    this.txtUserName.Text = user.UserName;
                //    this.txtFullName.Text = user.FullName != u.UserName ? u.FullName : "";
                //}
            }
        }
        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            //Mubble.User user = new User(Url.GetPathItem(1, string.Empty));
            //user.FullName = this.txtFullName.Text;
            //user.Save();
        }
    }
}
