using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mubble.Data
{
    internal delegate K LockLoader<T,K>(T key);
    internal static class LockHelper
    {
        public static K SyncGet<T, K>(this Dictionary<T, K> collection, T key, ReaderWriterLockSlim locker)
        {
            return SyncGet(collection, key, null, locker);
        }

        public static K SyncGet<T, K>(this Dictionary<T, K> collection, T key, LockLoader<T, K> loader, ReaderWriterLockSlim locker)
        {
            locker.EnterUpgradeableReadLock();
            try
            {
                if (collection.ContainsKey(key))
                {
                    return collection[key];
                }
                else if (loader != null)
                {
                    locker.EnterWriteLock();

                    try
                    {
                        if (collection.ContainsKey(key))
                        {
                            return collection[key];
                        }
                        else
                        {
                            K value = loader(key);
                            collection.Add(key, value);
                            return value;
                        }
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
                else
                {
                    return default(K);
                }
            }
            finally
            {
                locker.ExitUpgradeableReadLock();
            }
        }

        public static void SyncAdd<T, K>(this Dictionary<T, K> collection, T key, K value, ReaderWriterLockSlim locker)
        {
            locker.EnterWriteLock();

            try
            {
                collection.Add(key, value);
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }

        public static void SyncUpdate<T, K>(this Dictionary<T, K> collection, T key, K value, ReaderWriterLockSlim locker)
        {
            locker.EnterWriteLock();

            try
            {
                if (!collection.ContainsKey(key))
                {
                    collection.Add(key, value);
                }
                else
                {
                    collection[key] = value;
                }
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }
    }
}
