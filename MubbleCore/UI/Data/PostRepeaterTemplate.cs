using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Data
{
    public class PostRepeaterTemplate :
        Mubble.UI.RepeaterTemplate, IScoped
    {
        private object scope;
        public override object DataItem
        {
            get { return base.DataItem; }
            set { this.scope = (value != null) ? base.DataItem = value : this.scope; }
        }

        public PostRepeaterTemplate(int itemIndex, System.Web.UI.WebControls.ListItemType itemType) : base(itemIndex, itemType) { }

        #region IScoped Members

        public object Scope
        {
            get { return this.scope; }
        }

        #endregion
    }
}