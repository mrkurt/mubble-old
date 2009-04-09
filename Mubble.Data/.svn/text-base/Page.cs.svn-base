using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mubble.Data.Mapping;
using System.Linq.Expressions;
using System.Data.Linq;

namespace Mubble.Data
{
    public partial class Page : IData
    {
        internal Raw.Page Record { get; set; }

        static Mapping.Composer composer;
        static Mapping.PropertyMap map;
        static DataLoadOptions loadOptions;

        static Page()
        {
            composer = new Mubble.Data.Mapping.Composer
            {
                Properties = 
                    { 
                    new Alias { Property = new TypedName<Raw.Page>("Record"), Ignore = { "TextBlockID" } },
                    new Alias { Property = new TypedName<Raw.TextBlock>("TextBlock"), Through = "Record", Ignore = { "ID" } }
                    }
            };
            map = Mapping.PropertyMap.Convert<Page>(composer);

            loadOptions = new DataLoadOptions();
            loadOptions.LoadWith<Raw.Page>(p => p.TextBlock);
        }

        public static Page Get(Guid articleID, int pageNumber)
        {
            return Get(p => p.ContentItemID == articleID && p.PageNumber == pageNumber);
        }

        internal static Page Get(Expression<Func<Raw.Page, bool>> predicate)
        {
            return List(predicate).FirstOrDefault();
        }

        internal static List<Page> List(Expression<Func<Raw.Page, bool>> predicate)
        {
            using (var ctx = DatabaseHelper.GetReader(loadOptions))
            {
                var query = (from p in ctx.Pages orderby p.PageNumber select p).Where(predicate);
                return query.Select(p => new Page { Record = p }).ToList();
            }
        }

        #region IData Members

        public object this[string field]
        {
            get { return map.GetValue(field, this.Record); }
        }

        #endregion
    }
}
