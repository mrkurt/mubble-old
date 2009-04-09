using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Search;

namespace Mubble.Indexing
{
    public class SearchResults : IEnumerable<Hit>
    {
        public List<Hit> Hits { get; set; }

        internal static SearchResults FromRaw(Hits raw)
        {
            var hits = new List<Hit>(raw.Length());
            for (var i = 0; i < raw.Length(); i++)
            {
                hits.Add(Hit.FromRaw(raw, i));
            }

            return new SearchResults
            {
                Hits = hits
            };
        }

        #region IEnumerable<Hit> Members

        public IEnumerator<Hit> GetEnumerator()
        {
            return Hits.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
