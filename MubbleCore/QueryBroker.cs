using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

using Mubble.Models.QueryEngine;
using ActiveObjects;

namespace Mubble
{
    public delegate void CachedQueryChangedDelegate(string key);
    public static class QueryBroker
    {
        public static event CachedQueryChangedDelegate CachedQueryChanged;

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static NamedLock<string> queryLock = new NamedLock<string>();

        static int defaultTimeout = 1000;

        static DateTime lastIndexUpdate = DateTime.MinValue;

        static QueryBroker()
        {
            Mubble.Models.QueryEngine.Engine.SearcherRefreshed += new EngineActivityHandler(Engine_SearcherRefreshed);
        }

        static void Engine_SearcherRefreshed()
        {
            lastIndexUpdate = DateTime.Now;
        }

        public static QueryResults GetQueryResults(Query q, bool aggressiveCaching, out string cacheKey)
        {
            int startIndex = q.StartResultIndex;
            int endIndex = q.EndResultIndex;

            if (endIndex < 50 && aggressiveCaching)
            {
                q.StartResultIndex = 0;
                q.EndResultIndex = 50;
            }

            int hashCode = q.GetHashCode();

            cacheKey = CacheKey(q);

            QueryResults finalResults = null;
            Cacheable results = null;
            try
            {
                using (queryLock.Lock(cacheKey, defaultTimeout))
                {
                    results = HttpRuntime.Cache[cacheKey] as Cacheable;
                    if (results == null)
                    {
                        results = RunQueryAndCache(q, cacheKey);
                    }
                }
            }
            catch (NamedLock<Query>.TimeoutException timeout)
            {
                throw new QueryBrokerException(
                    string.Format("The timeout period expired when trying to retrieve query results: {0}", hashCode),
                    timeout
                    );
            }
            if (results == null || results.Results == null)
            {
                throw new QueryBrokerException("Unable to retrieve query results");
            }
            if (results.LastRunTime < lastIndexUpdate)
            {
                QueueQuery(q, cacheKey, results);
            }
            finalResults = QueryResults.BuildFromRawQuery(results.Results, CachedQueryObjectLoader);
            if (startIndex != finalResults.FirstIndex || endIndex != finalResults.LastIndex)
            {
                finalResults = finalResults.GetRange(startIndex, endIndex);
            }

            return finalResults;
        }

        static string CacheKey(Query q)
        {
            return string.Concat("Query[", q.GetHashCode(), "]");
        }

        static QueryResults CachedQueryObjectLoader(ContentList results)
        {
            QueryResults final = new QueryResults(results.StartIndex, results.EndIndex);
            final.TotalResults = results.TotalResults;

            Dictionary<string, Type> types = new Dictionary<string,Type>();

            foreach (Content c in results)
            {
                Type t = null;
                if(!types.TryGetValue(c.Type, out t))
                {
                    t = Mubble.Models.Tools.GetActiveObjectType(c.Type);
                    types.Add(c.Type, t);
                }
                if (t == null) continue;
                IActiveObject obj = DataBroker.GetActiveObject(t, c.ActiveObjectID);
                if (obj != null) final.Add(obj, c);
            }
            return final;
        }

        static Dictionary<string, DateTime> queuedQueries = new Dictionary<string, DateTime>();

        delegate Cacheable QueryRunner(Query q, string cacheKey, Cacheable current);
        static void QueueQuery(Query q, string cacheKey, Cacheable current)
        {
            lock (queuedQueries)
            {
                if (!queuedQueries.ContainsKey(cacheKey))
                {
                    queuedQueries.Add(cacheKey, DateTime.Now);

                    Worker.Queue(
                        new QueryRunner(SafeRunQueryAndCache),
                        q,
                        cacheKey,
                        current
                        );
                }
            }
        }

        static Cacheable SafeRunQueryAndCache(Query q, string cacheKey, Cacheable current)
        {
            try
            {
                return RunQueryAndCache(q, cacheKey, current);
            }
            catch (Exception ex)
            {
                log.Error("Running background query failed", ex);
                return null;
            }
        }

        static Cacheable RunQueryAndCache(Query q, string cacheKey)
        {
            return RunQueryAndCache(q, cacheKey, null);
        }

        static Cacheable RunQueryAndCache(Query q, string cacheKey, Cacheable current)
        {
            ContentList results = Content.Query(q);
            Cacheable updated = new Cacheable(results, DateTime.Now);

			if (current == null || current.Results != null || current.Results.Count < 2 || results.Count > 0)
			{
				CacheBroker.Set(cacheKey, updated, Config.Caching.Current.GetSlidingExpiration());
				if (CachedQueryChanged != null && (current == null || !results.Equals(current.Results)))
				{
					CachedQueryChanged(cacheKey);

				}
			}

            lock (queuedQueries)
            {
                queuedQueries.Remove(cacheKey);
            }

            return updated;
        }

        class Cacheable
        {
            public ContentList Results;
            public DateTime LastRunTime = DateTime.Now;

            public Cacheable(ContentList results, DateTime lastRunTime)
            {
                this.Results = results;
                this.LastRunTime = lastRunTime;
            }
        }
    }
}
