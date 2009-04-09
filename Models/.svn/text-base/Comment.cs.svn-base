using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models
{
    public class Comment
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string userLink;

        public string UserLink
        {
            get { return userLink; }
            set { userLink = value; }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string body;

        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        private DateTime postDate;

        public DateTime PostDate
        {
            get { return postDate; }
            set { postDate = value; }
        }

        public string PrettyDate
        {
            get { return this.PostDate.ToString("MMMM dd, yyyy @ hh:mmtt"); }
        }


        public Comment()
        {


        }
        public Comment(string username, string userlink, string title, string body, DateTime postdate)
        {
            this.UserName = username;
            this.UserLink = userlink;
            this.Title = title;
            this.Body = body;
            this.PostDate = postdate;
        }
    }
}
