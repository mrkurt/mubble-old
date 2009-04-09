using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mubble.Data.Mapping;
using Mubble.Data.Raw;
using System.Linq.Expressions;

namespace Mubble.Data
{
    public abstract class DataObject<T, K> : IData
        where T : DataObject<T,K>, new() 
        where K : class, IStandardRecord, new()
    {
        static Composer composer;
        static PropertyMap map;

        internal IRecord Record { get; set; }

        static DataObject()
        {
            var k = new K();
            composer = new Composer
            {
                Properties =
                {
                    new Alias { Property = new TypedName<K>("Record") }
                }
            };

            map = PropertyMap.Convert<T>(composer);
        }

        public object this[string name]
        {
            get
            {
                return map.GetValue(name, Record);
            }
        }

        public static T Get(Guid id)
        {
            return Get(a => a.ID == id);
        }

        internal static T Get(Expression<Func<K, bool>> predicate)
        {
            var result = new T { Record = DatabaseHelper.Get(predicate) };
            return result;
        }

        public static List<T> List()
        {
            return List(null);
        }

        internal static List<T> List(Expression<Func<K, bool>> predicate)
        {
            using (var ctx = DatabaseHelper.GetReader())
            {
                var query = (from i in ctx.GetTable<K>() select i);

                if(predicate != null)
                {
                    query.Where(predicate);
                }
                return (from i in query select new T { Record = i }).ToList();
            }
        }
    }
}
