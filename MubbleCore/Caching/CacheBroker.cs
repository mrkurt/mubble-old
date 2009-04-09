using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Caching
{
    public delegate T UncachedDataHandler<T>();
    public static class CacheBroker
    {
        public T GetData<T>(string key, UncachedDataHandler<T> handler) where T : class
        {

        }
    }
}
