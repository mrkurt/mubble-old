using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models.QueryEngine
{
    public delegate void EngineActivityHandler();
	public abstract class Engine
	{
        //public static event EngineActivityHandler SearcherReset;
        public static event EngineActivityHandler SearcherRefreshed;

        protected static void FireSearcherRefreshed()
        {
            if (SearcherRefreshed != null)
            {
                SearcherRefreshed();
            }
        }
        public static Engine CurrentEngine;

        public abstract void IndexDocument(IndexDocument document);

        public virtual IndexDocument[] Query(string query)
        {
            return this.Query(new Query(query));
        }

        public abstract void DeleteDocument(string id);

        public abstract IndexDocument[] Query(Query query);

        public abstract DateTime GetDateValue(IndexField field);

        public abstract string CacheFile{ get; }

        public abstract string GetIndexingStatus();

        public abstract void Optimize();

        public virtual void Abort()
        {

        }

        public static void Index(IndexDocument doc)
        {
            if (CurrentEngine != null) CurrentEngine.IndexDocument(doc);
        }

        public static DateTime DateValue(IndexField field)
        {
            return CurrentEngine.GetDateValue(field);
        }

        public static IndexDocument[] Search(string query, string defaultField, string orderBy, int resultCount)
        {
            Query q = new Query(query);
            q.DefaultField = defaultField;
            q.OrderBy = orderBy;
            q.StartResultIndex = 0;
            q.EndResultIndex = resultCount;
            return Search(new Query(query));
        }

        public static IndexDocument[] Search(Query query)
        {
            return CurrentEngine.Query(query);
        }

        public static void Delete(string documentID)
        {

        }
	}
}
