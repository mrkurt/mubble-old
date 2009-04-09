using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Threading;
using System.Data.Linq;
using Mubble.Data.Raw;

namespace Mubble.Data
{
    internal delegate DataLoadOptions LoadOptionsGetter();

    internal static class DatabaseHelper
    {
        public static DatabaseContext GetReader()
        {
            return GetReader(null);
        }
        public static DatabaseContext GetReader(DataLoadOptions options)
        {
            var ctx = new DatabaseContext();
            ctx.LoadOptions = options;
            ctx.ObjectTrackingEnabled = false;
            return ctx;
        }

        public static DatabaseContext GetWriter()
        {
            var ctx = new DatabaseContext();
            return ctx;
        }

        public static T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            using (var ctx = GetReader())
            {
                var query = ctx.GetTable<T>().Where(predicate);
                return query.FirstOrDefault();
            }
        }


        static readonly Dictionary<Type, Guid> resolvedTypes = new Dictionary<Type, Guid>();
        static readonly Dictionary<Guid, Type> resolvedTypeIDs = new Dictionary<Guid, Type>();
        static readonly ReaderWriterLockSlim typeLock = new ReaderWriterLockSlim();

        public static Guid GetTypeID(Type type)
        {
            return resolvedTypes.SyncGet(
                type, 
                key => 
                {
                    var t = Get<Raw.ContentType>(ct => ct.SystemType == key.Name);
                    return t != null ? t.ID : Guid.Empty;
                }, 
                typeLock
                );
        }

        static readonly Dictionary<Type, DataLoadOptions> typeLoadOptions = new Dictionary<Type, DataLoadOptions>();
        static readonly ReaderWriterLockSlim loadOptionsLock = new ReaderWriterLockSlim();
        public static DataLoadOptions GetLoadOptions<T>() where T : ContentItemBase<T>, new()
        {
            return typeLoadOptions.SyncGet(
                typeof(T),
                key => (new T()).GetLoadOptions(),
                loadOptionsLock
                );
        }
    }
}
