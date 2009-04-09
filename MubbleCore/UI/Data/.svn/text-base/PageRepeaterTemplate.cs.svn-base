using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Data
{
    public class PageRepeaterTemplate :
        Mubble.UI.RepeaterTemplate, IScoped
    {
        private object page;
        public override object DataItem
        {
            get { return base.DataItem; }
            set { this.page = (value != null) ? base.DataItem = value : this.page; }
        }

        public PageRepeaterTemplate(int itemIndex, System.Web.UI.WebControls.ListItemType itemType) : base(itemIndex, itemType) { }

        #region IScoped Members

        public object Scope
        {
            get { return this.page; }
        }

        #endregion
    }
}
