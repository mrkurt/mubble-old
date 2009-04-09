using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Data
{
    public abstract class BaseQueryFilter : IQueryFilter
    {
        #region IQueryFilter Members
        private IControl parent;
        public IControl Parent
        {
            get { return parent; }
            set { this.parent = value; }
        }

        public virtual Mubble.Models.QueryEngine.Query BuildQuery(Mubble.Models.QueryEngine.Query current) { return current; }

        public virtual void GetVisibleIndexes(Mubble.Models.QueryEngine.QueryResults results, List<int> visibleIndexes) {  }

        #endregion
    }
}
