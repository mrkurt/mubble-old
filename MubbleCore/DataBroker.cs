using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using System.Web.Caching;
using ActiveObjects;
using System.Collections;
using System.Threading;

namespace Mubble
{
    public static partial class DataBroker
    {
        //TODO: Replace ghetto locking with this
        static NamedLock<string> dbLock = new NamedLock<string>();
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static TimeSpan defaultLockTimeout = TimeSpan.FromSeconds(5);
        #region Cache retrieval

        public static Author GetAuthor(string name)
        {
            string displayName = name;
            name = name.ToLower();
            string key = string.Concat("Author[", name, "]");
            Author a = null;
            try
            {
                using (dbLock.Lock(key))
                {
                    a = CacheBroker.Get(key) as Author;
                    if (a == null)
                    {
                        a = Author.FindFirst(name);
                        if (a == null)
                        {
                            a = new Author();
                            a.ID = Guid.Empty;
                        }
                        Cache(a, key);
                    }
                }
            }
            catch (NamedLock<string>.TimeoutException)
            {
                log.InfoFormat("Author lock timed out: {0}", key);
            }
            if(a == null) a = new Author();
            if (a.ID == Guid.Empty)
            {
                a = new Author();
                a.UserName = name;
                a.DisplayName = displayName;
            }
            return a;
        }

        public static File GetFile(Guid controllerID, string fileName)
        {
            string key = string.Concat("File", GetControllerItemKey(controllerID, fileName));
            File f = null;
            try
            {
                using (dbLock.Lock(key))
                {
                    f = CacheBroker.Get(key) as File;
                    if (f == null)
                    {
                        f = File.FindFirst(controllerID, fileName);
                        Cache(f, key);
                    }
                }
            }
            catch (NamedLock<string>.TimeoutException)
            {
                log.InfoFormat("File lock timed out: {0}", key);
            }
            return f;
        }

        static string GetControllerItemKey(Guid controllerID, string item)
        {
            return string.Concat("[(", controllerID, ")(", item, ")]");
        }

        public static IActiveObject GetActiveObject(Type t, object id)
        {
            return GetActiveObject(t, id, true);
        }
        public static IActiveObject GetActiveObject(Type t, object id, bool loadIfNoCachedCopy)
        {
            if (id == null) return null;

            string cacheKey = CacheBroker.GetKey(t, id);
            IActiveObject obj = null;
            try
            {
                using (dbLock.Lock(cacheKey))
                {
                    obj = CacheBroker.Get(cacheKey) as IActiveObject;
                    if (obj == null)
                    {
                        obj = DataManager.ActiveObjectLoader(t, id);
                        if (obj != null)
                        {
                            Cache(obj);
                        }
                    }
                }
            }
            catch (NamedLock<string>.TimeoutException)
            {
                log.InfoFormat("ActiveObject lock timed out:", cacheKey);
            }
            return obj;
        }

        private static Controller GetControllerFromCache(string key)
        {
            bool isNull = false;
            return GetControllerFromCache(key, out isNull);
        }
        private static Controller GetControllerFromCache(string key, out bool isNull)
        {
            object obj = CacheBroker.Get(key);
            if (obj is NullCacheItem)
            {
                isNull = true;
                return null;
            }
            isNull = false;
            return obj as Controller;
        }
        #endregion

        #region Cache setting
        static string[] AliasBuilder(IActiveObject obj)
        {
            if (obj is Post)
            {
                Post p = obj as Post;
                return new string[] { string.Concat("Post", GetControllerItemKey(p.ControllerID, p.Slug)) };
            }
            if (obj is Controller)
            {
                return new string[] { string.Concat("Controller[", obj.DataManager["Path"], "]") };
            }
            if (obj is File)
            {
                File file = obj as File;
                return new string[] { string.Concat("File", GetControllerItemKey(file.ControllerID, file.FileName)) };
            }
            if (obj is Author)
            {
                Author a = obj as Author;
                if (a.UserName != null)
                {
                    return new string[] { string.Concat("Author[", a.UserName.ToLower(), "]") };
                }
            }
            return null;
        }
        static void Cache(IActiveCollection collection)
        {
            Cache(collection, true);
        }

        static void Cache(IActiveCollection collection, bool overwrite)
        {
            foreach (IActiveObject obj in collection)
            {
                Cache(obj, overwrite);
            }
        }

        static void CacheNull(string key)
        {
            CacheBroker.Set(key, CacheBroker.NullCacheItem, Config.Caching.Current.GetStandardExpiration());
        }

        private static void Cache(IActiveObject obj)
        {
            Cache(obj, true, AliasBuilder(obj));
        }
        private static void Cache(IActiveObject obj, bool overwrite)
        {
            Cache(obj, overwrite, AliasBuilder(obj));
        }
        private static void Cache(IActiveObject obj, params string[] aliases)
        {
            Cache(obj, true, aliases);
        }
        private static void Cache(IActiveObject obj, bool overwrite, params string[] aliases)
        {
            if (obj == null) return;
            RegisterSingleObjectLoader(obj);
            string cacheKey = CacheBroker.GetKey(obj);
            if (!overwrite && obj.GetType().IsInstanceOfType(CacheBroker.Get(cacheKey)))
            {
                if (aliases != null) CacheAliases(cacheKey, aliases);
                return;
            }

            CacheWithAliases(obj, cacheKey, aliases);
        }

        static void CacheWithAliases(IActiveObject obj, string key, params string[] aliases)
        {
            CacheBroker.Set(key, obj, null, Config.Caching.Current.GetSlidingExpiration(), aliases);
        }

        static void CacheAliases(string key, params string[] aliases)
        {
            if (aliases != null)
            {
                CacheBroker.SetAliases(key, aliases);
            }
        }

        private static void RegisterSingleObjectLoader(IActiveObject obj)
        {
            if (obj == null) return;
            obj.DataManager.SingleObjectLoader = GetActiveObject;
        }
        #endregion

        #region Utilities
        static MiscUtil.Threading.SyncLock warmupLock = new MiscUtil.Threading.SyncLock();
        public static void Warmup(params string[] controllerPaths)
        {
            using (warmupLock.Lock())
            {
                Dictionary<string, object> parameters = new Dictionary<string,object>();
                parameters.Add("RowIndex_start", 0);
                parameters.Add("RowIndex_end", 80);
                parameters.Add("Status", 1);

                ActiveCollection<Controller> controllers = Controller.Find(parameters);

                Cache(controllers);

                ActiveCollection<Post> posts = Post.Find(parameters);
                Cache(posts);
                foreach (Post p in posts)
                {
                    foreach (string author in p.Authors)
                    {
                        Author a = GetAuthor(author);
                    }
                    Discussion d = p.Discussion;

                    controllers.Add(p.Controller);
                }

                foreach (string path in controllerPaths)
                {
                    Controller c = GetController(path);
                    if (c == null) continue;
                    controllers.Add(c);
                }

                foreach (Controller c in controllers)
                {
                    foreach (string author in c.Authors)
                    {
                        Author a = GetAuthor(author);
                    }
                    Route r = c.Route;
                    Template t = c.Template;
                    ModuleControl m = c.ModuleControl;
                    Module module = m.Module;
                    ActiveCollection<RssFeed> feeds = c.RssFeeds;
                }
            }
        }
        #endregion
    }
}
