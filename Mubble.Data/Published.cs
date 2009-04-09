using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.Data
{
    public class Published<T> : ContentItemBase<T>, IPublished
        where T : Published<T>, new()
    {
        // Has metadata
        // Has authors

        public List<Author> Authors
        {
            get
            {
                using (var ctx = DatabaseHelper.GetReader())
                {
                    var authors = from a in ctx.ContentItemAuthors
                                  where a.ContentItemID == this.ID
                                  select new Author { Record = a.Author, Alias = a.AuthorID.ToString() };

                    return authors.ToList();
                }
            }
        }
    }
}
