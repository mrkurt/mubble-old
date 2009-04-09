using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using ActiveObjects;
using Mubble.Models.QueryEngine;
using System.Web.Caching;

namespace Mubble.UI.Data
{
    //TODO: Finish out index data source
    public class Index : IDataSource
    {
        private List owner;

        public List Parent
        {
            get { return owner; }
            set { owner = value; }
        }

        private bool aggressiveCaching = true;

        public bool AggressiveCaching
        {
            get { return aggressiveCaching; }
            set { aggressiveCaching = value; }
        }

        private bool cacheAgainstQuery = true;

        public bool CacheAgainstQuery
        {
            get { return cacheAgainstQuery; }
            set { cacheAgainstQuery = value; }
        }


        private string group;

        public string Group
        {
            get { return group; }
            set { group = value; }
        }

        private bool async = false;

        public bool Asynchronous
        {
            get { return async; }
            set { async = value; }
        }


        private List<IQueryFilter> filters = new List<IQueryFilter>();

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<IQueryFilter> Filters
        {
            get { return filters; }
            set { filters = value; }
        }

        private List<Guid> r = new List<Guid>();
        public List<Guid> Results
        {
            get { return r; }
        }

        private string orderBy = "PublishDate DESC";

        public string OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }

        #region IDataSource Members

        public IActiveCollection GetData(int startIndex, int endIndex)
        {
            Query q = new Query();
            q.OrderBy = this.OrderBy;
            q.StartResultIndex = startIndex;
            if (endIndex > startIndex)
            {
                q.EndResultIndex = endIndex;
                q.EndResultIndex += this.GroupItems.Count;
            }

            foreach (IQueryFilter filter in this.Filters)
            {
                filter.Parent = this.Parent;
                q = filter.BuildQuery(q);
            }

            if (q.IsValid)
            {
                q.RestrictByGroups(IndexPermissions.View, Security.User.GetRoles());

                string queryKey = null;
                QueryResults qr = QueryBroker.GetQueryResults(q, this.AggressiveCaching, out queryKey);
                    
                    //QueryBroker.GetQueryResults(q, !this.Asynchronous, this.AggressiveCaching, out queryKey);

                if (this.CacheAgainstQuery)
                {
                    Parent.Page.SetQueryResultDependency(queryKey);
                }
                if (qr == null) return null;

                return qr;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<IActiveObject> FilterData(IActiveCollection data)
        {
            QueryResults results = data as QueryResults;
            if (results == null) return (IEnumerable<IActiveObject>)data;
            List<int> visibleIndexes = new List<int>(data.Count);
            for (int i = 0; i < data.Count; i++)
            {
                visibleIndexes.Add(i);
            }
            foreach (IQueryFilter filter in this.Filters)
            {
                filter.GetVisibleIndexes(results, visibleIndexes);
            }
            List<IActiveObject> list = new List<IActiveObject>(visibleIndexes.Count);
            
            foreach (int i in visibleIndexes)
            {
                IActiveObject obj = results[i];
                Guid aid = results.GetQueryObject(i).ActiveObjectID;
                if (!this.GroupItems.Contains(aid))
                {
                    list.Add(obj);
                }
                else
                {
                    this.r.Add(aid);
                }
            }
            return list;
        }

        #endregion

        List<Guid> groupItems;
        List<Guid> GroupItems
        {
            get
            {
                if (groupItems != null) return groupItems;
                List<Guid> items = new List<Guid>();

                if (!string.IsNullOrEmpty(this.Group))
                {
                    foreach (List list in Mubble.UI.Control.FindMatchingControls(this.Parent))
                    {
                        foreach (Index i in list.Data)
                        {
                            if (!this.Group.Equals(i.Group, StringComparison.CurrentCultureIgnoreCase)) continue;
                            items.AddRange(i.Results);
                        }
                    }
                }
                groupItems = items;
                return items;
            }
        }
    }

}
