using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;

namespace Mubble.UI.Html
{
    public class LinkFromMetadata : HyperLink
    {
        private string field;

        public string Field
        {
            get { return field; }
            set { field = value; }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            IMetaData scope = Control.GetCurrentScope<IMetaData>(this);
            if (scope != null && scope.MetaData != null && this.Field != null)
            {
                this.NavigateUrl = scope.MetaData.GetFirstStringValue(this.Field);
            }
            if (this.NavigateUrl != null && this.NavigateUrl.Length > 0)
            {
                base.Render(writer);
            }
        }
    }
}
