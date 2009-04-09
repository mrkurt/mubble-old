using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.Data
{
    public partial class Section : ContentItemBase<Section>
    {
        public List<ContentItem> List(DateTime start, DateTime end)
        {
            var options = DatabaseHelper.GetLoadOptions<ContentItem>();
            using (var ctx = DatabaseHelper.GetReader(options))
            {
                var query = from c in ctx.ContentItems
                            where 
                                c.PublishDate >= start && 
                                c.PublishDate <= end && 
                                c.ContentItemID == this.ID
                            orderby c.PublishDate descending
                            select new ContentItem { Record = c };

                return query.ToList();
            }
        }

        public PagedList<IContentItem> List(int page, int pageSize)
        {
            var options = DatabaseHelper.GetLoadOptions<ContentItem>();
            using (var ctx = DatabaseHelper.GetReader(options))
            {
                var query = from c in ctx.ContentItems
                            where c.ContentItemID == this.ID
                            orderby c.PublishDate descending
                            select c;

                return PagedList<ContentItem>.CreateForQuery<Raw.ContentItem, IContentItem>(
                    query, 
                    ci => new ContentItem { Record = ci },
                    page, 
                    pageSize
                    );
            }
        }
    }
}
