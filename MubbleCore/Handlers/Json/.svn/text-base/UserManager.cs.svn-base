using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.Web.Profile;

namespace Mubble.Handlers.Json
{
    public class UserManager : Base
    {
        public override string ProcessUncachedRequest(System.Web.HttpContext context)
        {
            switch (this.Url.GetPathItem(0, "").ToLower())
            {
                case "search":
                    this.AssertPermission("full");
                    string query = context.Request.QueryString["value"];
                    if (query != null && query.Length > 0)
                    {
                        int totalRecords = 0;
                        MembershipUserCollection users = Membership.FindUsersByName(query + "%", 0, 10, out totalRecords);
                        return this.GetJsonOutput(new UserList(users, totalRecords));
                    }
                    break;
            }
            return null;
        }
    }
    public class UserList
    {
        private int totalRecords = 0;

        public int TotalRecords
        {
            get { return totalRecords; }
            set { totalRecords = value; }
        }

        private List<User> users = new List<User>();

        public List<User> Users
        {
            get { return users; }
        }

        public UserList(MembershipUserCollection users, int totalRecords)
        {
            this.TotalRecords = totalRecords;
            foreach(MembershipUser u in users)
            {
                this.Users.Add(new User(u));
            }
        }
    }
    public class User
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string fullName;

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }


        public User(MembershipUser user)
        {
            this.UserName = user.UserName;
            ProfileBase profile = ProfileBase.Create(user.UserName);
            this.FullName = profile.GetPropertyValue("FullName") as string;
            this.Email = user.Email;
        }
    }
}
