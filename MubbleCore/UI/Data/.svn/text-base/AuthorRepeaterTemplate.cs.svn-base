using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using System.Web.Configuration;

namespace Mubble.UI.Data
{
    public class AuthorRepeaterTemplate : 
        Mubble.UI.RepeaterTemplate, IScoped, ILinkable
    {
        private object scope;
        public override object DataItem
        {
            get { return base.DataItem; }
            set { this.scope = (value != null) ? base.DataItem = value : this.scope; }
        }

        public AuthorRepeaterTemplate(int itemIndex, System.Web.UI.WebControls.ListItemType itemType) : base(itemIndex, itemType) { }

        #region IScoped Members

        public object Scope
        {
            get { return this.scope; }
        }

        #endregion

        #region ILinkable Members

        /// <summary>
        /// Gets the Link to the author profile
        /// </summary>
        public Link Url
        {
            get 
            {
                Author author = scope as Author;
                Link l = new Link();
                if (author != null && author.Exists)
                {
                    l.Path = WebConfigurationManager.AppSettings["AuthorController"];
                    l.Extra = "/" + this.Context.Server.UrlEncode(author.UserName);
                }
                if (author == null)
                {
                    l = null;
                }
                else
                {
                    l.Title = author.DisplayName != null ? author.DisplayName : author.UserName;
                }
                return l;
            }
        }

        #endregion
    }
}
