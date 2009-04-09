using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using System.ComponentModel;
using System.Web;

namespace Mubble.UI.WebControls
{
    public class AuthorLink : HyperLink
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        #region Other hidden properties
        [Browsable(false)]
        public override string ContentPath
        {
            get{return base.ContentPath;}
            set{base.ContentPath = value;}
        }

        [Browsable(false)]
        public override string ContentPathExtra
        {
            get { return base.ContentPathExtra; }
            set { base.ContentPathExtra = value; }
        }

        [Browsable(false)]
        public override Link UrlPair
        {
            get { return base.UrlPair; }
            set { base.UrlPair = value; }
        }
        #endregion

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (this.UserName != null)
            {
                Author a = DataBroker.GetAuthor(this.UserName);
                if (a == null)
                {
                    a = new Author();
                    a.DisplayName = this.UserName;
                }

                this.DoLink = a.ID != Guid.Empty;

                //if (this.DoLink)
                //{
                    this.ContentPath = "/authors";
                    this.ContentPathExtra = "/" + HttpContext.Current.Server.UrlEncode(this.UserName.ToLower());
                //}


                if (this.Text == null || this.Text.Length == 0)
                {
                    this.Text = (a.ID != Guid.Empty) ? a.DisplayName : this.UserName;
                }
            }
            base.Render(writer);
        }
    }
}
