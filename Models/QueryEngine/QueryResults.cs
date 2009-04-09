using System;
using System.Collections.Generic;
using System.Text;

using ActiveObjects;

namespace Mubble.Models.QueryEngine
{
    public delegate QueryResults QueryResultBatchLoaderDelegate(ContentList items);
    public class QueryResults : ActiveCollection<IActiveObject>
    {
        public QueryResults(int firstIndex, int lastIndex)
        {
            this.firstIndex = firstIndex;
            this.lastIndex = lastIndex;
        }

        ContentList rawResults = new ContentList();
        public void Add(Content content)
        {
            IActiveObject obj = null;
            Type t = Type.GetType(content.Type, false, true);
            if (t == null) return;            
            obj = DefaultItemLoader(t, content);
            if (obj != null)
            {
                this.Add(obj, content);
            }
        }

        static IActiveObject DefaultItemLoader(Type t, Content c)
        {
            IActiveObject obj = Activator.CreateInstance(t) as IActiveObject;
            if (obj != null && obj.DataManager.Load(obj.DataManager.PrimaryKeyField, c.ActiveObjectID))
            {
                return obj;
            }
            return null;
        }

        public static QueryResults DefaultBatchLoader(ContentList items)
        {
            QueryResults final = new QueryResults(items.StartIndex, items.EndIndex);
            final.TotalResults = items.TotalResults;
            foreach (Content c in items)
            {
                final.Add(c);
            }
            return final;
        }

        public static QueryResults BuildFromRawQuery(ContentList results)
        {
            return BuildFromRawQuery(results, null);
        }

        public static QueryResults BuildFromRawQuery(ContentList results, QueryResultBatchLoaderDelegate loader)
        {
            if (loader != null) return loader(results);
            return DefaultBatchLoader(results);
        }

        //public static QueryResults BuildFromRawQuery(ContentList results, QueryResultItemLoaderDelegate loader)
        //{
        //    if (loader == null) loader = DefaultLoader;

        //    QueryResults final = new QueryResults(results.StartIndex, results.EndIndex);
        //    foreach (Content c in results)
        //    {
        //        final.Add(c, loader);
        //    }

        //    final.TotalResults = results.TotalResults;

        //    return final;
        //}

        public void Add(IActiveObject obj, Content content)
        {
            this.Add(obj);
            rawResults.Add(content);
        }

        public Content GetQueryObject(int i)
        {
            if (i >= 0 && i < rawResults.Count)
            {
                return rawResults[i];
            }
            return null;
        }

        public Content GetQueryObject(IActiveObject obj)
        {
            int index = this.IndexOf(obj);
            return this.GetQueryObject(index);
        }

        private int firstIndex;

        public int FirstIndex
        {
            get { return firstIndex; }
            set { firstIndex = value; }
        }

        private int lastIndex;

        public int LastIndex
        {
            get { return lastIndex; }
            set { lastIndex = value; }
        }


        public QueryResults GetRange(int startIndex, int endIndex)
        {
            QueryResults results = new QueryResults(startIndex, endIndex);
            results.TotalResults = this.TotalResults;

            for (int i = startIndex; i < endIndex && i < this.Count; i++)
            {
                results.Add(this[i], this.GetQueryObject(i));
            }
            return results;
        }

        public static QueryResults Retrieve(Query q)
        {
            return Retrieve(q, null);
        }

        public static QueryResults Retrieve(Query q, string[] groups)
        {
            return Retrieve(q, groups, new string[]{"View"});
        }

        public static QueryResults Retrieve(Query q, string[] groups, string[] permissions)
        {
            ContentList results = Content.Query(q, groups, permissions);
            return BuildFromRawQuery(results);
        }
    }
}
