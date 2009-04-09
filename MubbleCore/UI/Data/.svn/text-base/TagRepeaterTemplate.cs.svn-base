using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using System.Web.Configuration;

namespace Mubble.UI.Data
{
    public class TagRepeaterTemplate :
        Mubble.UI.RepeaterTemplate, IScoped, ILinkable
    {
        private object scope;
        public override object DataItem
        {
            get { return base.DataItem; }
            set { this.scope = (value != null) ? base.DataItem = value : this.scope; }
        }

        public TagRepeaterTemplate(int itemIndex, System.Web.UI.WebControls.ListItemType itemType) : base(itemIndex, itemType) { }

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
                Tag tag = scope as Tag;
                if (tag != null)
                {
                    Link link = new Link(WebConfigurationManager.AppSettings["SearchController"]);
                    link.Params[tag.Name] = (tag.StringValueNormalized != null) ? (object)tag.StringValueNormalized : (object)tag.NumericValue;
                    link.Title = (tag.StringValue != null) ? tag.StringValue : tag.NumericValue.ToString();

                    return link;
                }
                return null;
            }
        }

        #endregion
    }
}
