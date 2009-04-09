using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Data
{
    public class FilterBase : IFilter
    {
        #region IFilter Members

        private IControl parent;

        public IControl Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public virtual void Before(Dictionary<string, object> parameters)
        {

        }

        #endregion
    }
}
