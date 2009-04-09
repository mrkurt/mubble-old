using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Mubble.Data
{
    public interface IPagedList : IEnumerable<IData>
    {
        IList<IData> Items { get; }
        int TotalCount { get; }
        int CurrentPage { get; }
        int PageSize { get; }
    }
    public class PagedList<T> : IPagedList
        where T : IData
    {
        public IList<T> Items { get; private set; }
        public int TotalCount { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }

        public PagedList(IList<T> items, int totalCount, int currentPage, int pageSize)
        {
            this.Items = items;
            this.TotalCount = totalCount;
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
        }

        internal static PagedList<TResult> CreateForQuery<TRaw,TResult>(IQueryable<TRaw> query, Expression<Func<TRaw,TResult>> selector, int page, int pageSize)
            where TResult : IData
        {
            var start = (page - 1) * pageSize;
            var end = start + pageSize;
            var total = query.Count();
          
            var list = query.Skip(start).Take(pageSize).Select(selector).ToList();

            return new PagedList<TResult>(list, total, page, pageSize);
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region IPagedList Members

        IList<IData> IPagedList.Items
        {
            get 
            {
                return new List<IData>((IEnumerable<IData>)this);
            }
        }

        #endregion

        #region IEnumerable<IData> Members

        IEnumerator<IData> IEnumerable<IData>.GetEnumerator()
        {
            foreach (var i in this.Items)
            {
                yield return i;
            }
        }

        #endregion
    }
}
