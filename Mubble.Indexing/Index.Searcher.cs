using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System.Threading;
using Lucene.Net.Analysis.Standard;

namespace Mubble.Indexing
{
	public partial class Index
	{
        class Searcher : IDisposable
        {
            private readonly Index index;

            private string path { get { return index.Path; } }

            public Searcher(Index index)
            {
                this.index = index;
                this.index.Changed += ResetSearcher;
                this.ResetSearcher();
            }

            IndexSearcher searcher;
            private readonly ReaderWriterLockSlim searcherLock = new ReaderWriterLockSlim();
            public SearchResults Search(Query query)
            {
                searcherLock.EnterReadLock();

                try
                {
                    var lquery = QueryTranslator.Translate(query);
                    var results = this.searcher.Search(lquery);
                    return SearchResults.FromRaw(results);
                }
                finally
                {
                    searcherLock.ExitReadLock();
                }
            }

            static DateTime lastReset = DateTime.MinValue;

            void ResetSearcher()
            {
                var start = DateTime.Now;
                searcherLock.EnterWriteLock();
                try
                {
                    if (lastReset > start) return;

                    lastReset = DateTime.Now;
                    var newSearcher = new IndexSearcher(
                        MMapDirectory.GetDirectory(this.path, false)
                    );

                    var oldSearcher = this.searcher;
                    this.searcher = newSearcher;

                    if (oldSearcher != null)
                    {
                        oldSearcher.Close();
                    }
                }
                finally
                {
                    searcherLock.ExitWriteLock();
                }
            }

            #region IDisposable Members

            public void Dispose()
            {
                // Dispose of searcher crap here
            }

            #endregion
        }

	}
}
