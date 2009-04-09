using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

using ActiveObjects;
using Mubble.Models;

namespace Mubble
{
    public static class CacheBroker
    {
        public static NullCacheItem NullCacheItem = new NullCacheItem();

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static bool needsInit = true;
        public static void Init()
        {
            if (needsInit)
            {
                needsInit = false;
                ActiveObjects.DataManager.ObjectSaved += new ActiveObjects.DataManagerEventHandler(ActiveObjectSaved);

                if (Config.Cluster.Current.Enabled)
                {
                    //TODO: Move the clustering stuff out of cache broker
                    //Mubble.MessageQueue.Manager.RegisterSender("mubble", Config.Cluster.Current.MulticastAddress);

                    //Mubble.MessageQueue.Manager.RegisterHandler<RemoveInstructions>(CacheBroker.Remove);

                    //Mubble.MessageQueue.Manager.Watch(Config.Cluster.Current.LocalQueue);
                }
            }
        }

        static void ActiveObjectSaved(DataManager dm, DataManagerEventArgs e)
        {
            try
            {
                string key = CacheBroker.GetKey(dm.Settings.ActiveObjectType, dm.PrimaryKeyValue);
                if (key != null)
                {
                    List<string> keys = new List<string>();
                    keys.Add(key);

                    object obj = CacheBroker.Get(key);

                    if (obj is IActiveObject)
                    {
                        IActiveObject iao = obj as IActiveObject;
                        while (iao != null)
                        {
                            keys.Add(GetKey(iao));
                            //HttpRuntime.Cache.Remove(GetKey(iao));

                            if (iao.DataManager["ControllerID"] is Guid)
                            {
                                iao = DataBroker.GetController((Guid)iao.DataManager["ControllerID"]);
                                //iao = Get(GetKey(typeof(Controller), iao.DataManager["ControllerID"])) as IActiveObject;
                            }
                            else
                            {
                                iao = null;
                            }
                        }
                        iao = null;
                    }
                    obj = null;

                    Remove(keys);
                }
            }
            catch(Exception ex)
            {
                log.Error("Error occurred while expiring content", ex);
            }
        }

        public static string GetKey(IActiveObject obj)
        {
            return GetKey(obj.GetType(), obj.DataManager.PrimaryKeyValue);
        }

        public static string GetKey(Type activeObjectType, object primaryKey)
        {
            return string.Concat(ActiveObjects.DataManager.GetBaseActiveObjectType(activeObjectType).Name, "[", primaryKey, "]");
        }

        public static object Get(string key)
        {
            object obj = HttpRuntime.Cache[key];
            if (obj is CachePointer)
            {
                obj = HttpRuntime.Cache[((CachePointer)obj).NewKey];
            }
            return obj;
        }

        public static void Remove(IEnumerable<string> keys)
        {
            if (keys != null)
            {
                Remove((new List<string>(keys)).ToArray());
            }
        }

        public static void Remove(params string[] keys)
        {
            Remove(new RemoveInstructions { Keys = keys, Origin = Environment.MachineName }, true);
        }

        static void Remove(RemoveInstructions instructions)
        {
            bool local = string.Equals(
                        Environment.MachineName,
                        instructions.Origin,
                        StringComparison.CurrentCultureIgnoreCase
                  );


            if (!local)
            {
                Remove(instructions, local);
            }
        }

        static void Remove(RemoveInstructions instructions, bool local)
        {
            if (instructions.Keys == null) return;

            //if (Config.Cluster.Current.Enabled && local)
            //{
            //    Mubble.MessageQueue.Manager.Send("mubble", instructions);
            //}
            //else
            //{
                foreach (string key in instructions.Keys)
                {
                    HttpRuntime.Cache.Remove(key);
                }
            //}
        }

        [Serializable]
        public class RemoveInstructions
        {
            public string[] Keys { get; set; }
            public string Origin { get; set; }
        }

        public static void Set(string key, object data, DateTime expiration)
        {
            Set(key, data, null, expiration);
        }

        public static void Set(string key, object data, TimeSpan expiration)
        {
            Set(key, data, null, expiration);
        }

        public static void Set(string key, object data, System.Web.Caching.CacheDependency cacheDependencies, DateTime expiration)
        {
            Set(key, data, cacheDependencies, expiration, new string[0]);
        }

        public static void Set(string key, object data, System.Web.Caching.CacheDependency cacheDependencies, TimeSpan expiration)
        {
            Set(key, data, cacheDependencies, expiration, new string[0]);
        }

        public static void Set(string key, object data, System.Web.Caching.CacheDependency cacheDependencies, DateTime expiration, params string[] aliases)
        {
            HttpRuntime.Cache.Insert(key, data, cacheDependencies, expiration, System.Web.Caching.Cache.NoSlidingExpiration);
            SetAliases(key, data, aliases);
        }

        public static void Set(string key, object data, System.Web.Caching.CacheDependency cacheDependencies, TimeSpan expiration, params string[] aliases)
        {
            HttpRuntime.Cache.Insert(key, data, cacheDependencies, System.Web.Caching.Cache.NoAbsoluteExpiration, expiration);
            SetAliases(key, data, aliases);
        }

        public static void SetAliases(string key, object data, params string[] aliases)
        {
            if (aliases != null && aliases.Length > 0)
            {
                foreach (string alias in aliases)
                {
                    HttpRuntime.Cache.Insert(alias, data, new System.Web.Caching.CacheDependency(null, new string[] { key }));
                }
            }
        }
    }
}
