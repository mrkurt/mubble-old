using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Mubble
{
    public class QueryResultDependency : System.Web.Caching.CacheDependency
    {
        private string key;

        CachedQueryChangedDelegate handler = null;

        public QueryResultDependency(string key)
        {
            this.key = key;
            if (handler == null)
            {
                handler = new CachedQueryChangedDelegate(QueryBroker_CachedQueryChanged);
                QueryBroker.CachedQueryChanged += handler;
            }
        }

        void QueryBroker_CachedQueryChanged(string key)
        {
            if (key.Equals(this.key, StringComparison.CurrentCultureIgnoreCase))
            {
                base.NotifyDependencyChanged(this, new EventArgs());
            }
        }

        protected override void DependencyDispose()
        {
            this.key = null;
            if (handler != null)
            {
                QueryBroker.CachedQueryChanged -= handler;
                handler = null;
            }
            base.DependencyDispose();
        }

        public override int GetHashCode()
        {
            return key.GetHashCode();
        }
    }
}
