using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mubble.Data.Raw
{
    internal static class RecordHelper
    {
        static Dictionary<Type, Dictionary<string, IColumn>> properties = new Dictionary<Type, Dictionary<string, IColumn>>();
        static ReaderWriterLockSlim propertiesLock = new ReaderWriterLockSlim();

        public static object Get<T>(T obj, string field) where T : IRecord
        {
            var col = GetColumn(typeof(T), field);
            return col.GetValue(obj);
        }

        public static K Get<T, K>(T obj, string field) where T : IRecord
        {
            var col = GetColumn(typeof(T), field) as Column<T, K>;
            if (col == null)
            {
                throw new InvalidOperationException("There is no column that returns type " + typeof(K).FullName);
            }
            return col.Getter(obj);
        }

        public static IColumn GetColumn(Type type, string field)
        {
            var t = properties.SyncGet(type, propertiesLock);
            return t.SyncGet(field, propertiesLock);
        }

        public static void Set<T>(T obj, string field, object value) where T : IRecord
        {
            var t = properties.SyncGet(typeof(T), propertiesLock);
            var col = t.SyncGet(field, propertiesLock);
            col.SetValue(obj, value);
        }

        public static void RegisterColumn<T, K>(string name, Getter<T, K> getter, Setter<T, K> setter) where T : IRecord
        {
            var t = properties.SyncGet(
                        typeof(T),
                        key => new Dictionary<string, IColumn>(StringComparer.CurrentCultureIgnoreCase),
                        propertiesLock
                        );

            t.SyncAdd(
                name,
                new Column<T, K> { Name = name, Getter = getter, Setter = setter },
                propertiesLock
                );
        }

        public static void RegisterAssociation<T, K>(string name, Getter<T, K> getter, Setter<T, K> setter)
            where T : IRecord
            where K : IRecord
        {
            var t = properties.SyncGet(
            typeof(T),
            key => new Dictionary<string, IColumn>(StringComparer.CurrentCultureIgnoreCase),
            propertiesLock
            );

            t.SyncAdd(
                name,
                new Association<T, K> { Getter = getter, Setter = setter },
                propertiesLock
                );
        }

        public static List<string> GetColumnNames<T>()
        {
            return GetColumnNames(typeof(T));
        }

        public static List<string> GetColumnNames(Type type)
        {
            return GetColumnNames(type, true);
        }
        public static List<string> GetColumnNames(Type type, bool includeAssociations)
        {
            var cols = GetColumns(type, includeAssociations);
            var names = new List<String>(cols.Count);
            cols.ForEach(c => names.Add(c.Name));
            return names;
        }

        public static List<IColumn> GetColumns(Type type)
        {
            return GetColumns(type, true);
        }

        public static List<IColumn> GetColumns(Type type, bool includeAssociations)
        {
            var t = properties.SyncGet(type, propertiesLock);

            propertiesLock.EnterReadLock();
            try
            {
                var cols = new List<IColumn>();
                foreach (var key in t.Keys)
                {
                    if (!(t[key] is IAssociation))
                    {
                        cols.Add(t[key]);
                    }
                }
                return cols;
            }
            finally
            {
                propertiesLock.ExitReadLock();
            }
        }

        public static K GetColumnValue<T, K>(T obj, string name) where T : IRecord
        {

            var col = GetColumn<T,K>(name);
            return col.Getter(obj);
        }

        public static void SetColumnValue<T, K>(T obj, string name, K value) where T : IRecord
        {
            var col = GetColumn<T, K>(name);
            col.Setter(obj, value);
        }

        static Column<T, K> GetColumn<T, K>(string name) where T : IRecord
        {
            var t = properties.SyncGet(typeof(T), propertiesLock);
            var col = t.SyncGet(name, propertiesLock);

            if (col is Column<T, K>)
            {
                return (Column<T, K>)col;
            }
            else
            {
                throw new ArgumentException(
                    string.Format("There is no column for {0} with type {1}", typeof(T).FullName, typeof(K).FullName)
                    );
            }
        }
    }
}
