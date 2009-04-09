using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mubble.Data;

namespace Mubble.Web
{
    public interface IResponse
    {
        IContentItem CurrentContent { get; }
    }
    public abstract class Response : IResponse
    {
        public IContentItem CurrentContent { get; set; }

        public interface ISingleItem : IResponse
        {
            IData Item { get; }
        }
        public class SingleItem<T> : Response, ISingleItem
            where T : IData
        {
            public T Item { get; set; }
            public SingleItem(T data) : this(data, data as IContentItem){ }

            public SingleItem(T data, IContentItem currentContent)
            {
                this.CurrentContent = currentContent ?? data as IContentItem;
                this.Item = data;

                if (this.CurrentContent == null)
                {
                    throw new InvalidOperationException(
                        "You must explicitly pass a parent content item if \"data\" does not implement IContentItem"
                        );
                }
            }

            #region ISingleItem Members

            IData ISingleItem.Item
            {
                get { return this.Item; }
            }

            #endregion
        }

        public interface IList : IResponse
        {
            IPagedList Items { get; }
        }
        public class List<T> : Response, IList
            where T : IData
        {
            public PagedList<T> Items { get; set; }

            public List(PagedList<T> items, IContentItem currentContent)
            {
                this.Items = items;
                this.CurrentContent = currentContent;

                if (this.CurrentContent == null)
                {
                    throw new ArgumentNullException("currentContent");

                }
            }

            #region IList Members

            IPagedList IList.Items
            {
                get { return Items; }
            }

            #endregion
        }
    }
}
