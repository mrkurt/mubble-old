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
        public static Controller GetController(Guid id)
        {
            string key = string.Concat("Controller[", id, "]");
            Controller c = null;
            try
            {
                using (dbLock.Lock(key))
                {
                    bool isNull = false;
                    c = GetControllerFromCache(key, out isNull);

                    if (c == null && !isNull)
                    {
                        c = Controller.FindFirst(id);
                        if (c != null)
                        {
                            Cache(c, string.Concat("Controller[", c.Path, "]"));
                        }
                        else
                        {
                            CacheNull(key);
                        }
                    }
                }
            }
            catch (NamedLock<string>.TimeoutException)
            {
                log.InfoFormat("Controller lock timed out: {0}", key);
            }
            return c;
        }

        public static Controller GetController(string path)
        {
            string key = string.Concat("Controller[", path, "]");
            Controller c = null;
            try
            {
                using (dbLock.Lock(key))
                {
                    bool isNull = false;
                    c = GetControllerFromCache(key, out isNull);

                    if (c == null && !isNull)
                    {
                        c = Controller.FindFirst(path);
                        if (c != null)
                        {
                            Cache(c, key);
                        }
                        else
                        {
                            CacheNull(key);
                        }
                    }
                }
            }
            catch (NamedLock<string>.TimeoutException)
            {
                log.InfoFormat("Controller lock timed out: {0}", key);
            }
            return c;
        }
    }
}
