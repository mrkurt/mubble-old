using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using MiscUtil.Threading;

namespace Mubble.Indexing
{
    public partial class Index
    {
        class Writer : IDisposable
        {
            public event IndexChanged Changed;

            void FireChanged()
            {
                if (Changed != null)
                {
                    Changed();
                }
            }

            CustomThreadPool pool;
            private readonly Index index;

            private string path { get { return index.Path; } }

            public Writer(Index index)
            {
                this.index = index;
                pool = new MiscUtil.Threading.CustomThreadPool(index.Path);
                pool.SetMinMaxThreads(0, 5);
                pool.IdlePeriod = 10;
                pool.WorkerThreadPriority = ThreadPriority.BelowNormal;
                pool.WorkerThreadsAreBackground = false;
                pool.BeforeWorkItem += new BeforeWorkItemHandler(pool_BeforeWorkItem);
                pool.AfterWorkItem += new AfterWorkItemHandler(pool_AfterWorkItem);
                pool.WorkerException += new ThreadPoolExceptionHandler(pool_WorkerException);
            }

            void pool_BeforeWorkItem(CustomThreadPool pool, ThreadPoolWorkItem workItem, ref bool cancel)
            {
                var mine = workItem.Parameters[0] as IndexWorkItem;

                if (mine != null)
                {
                    mine.FireBeforeRun();
                }
            }

            void pool_WorkerException(CustomThreadPool pool, ThreadPoolWorkItem workItem, Exception e, ref bool handled)
            {
                var mine = workItem.Parameters[0] as IndexWorkItem;

                if (mine != null)
                {
                    mine.FireError(e, ref handled);
                }
            }

            void pool_AfterWorkItem(CustomThreadPool pool, ThreadPoolWorkItem workItem)
            {
                var mine = workItem.Parameters[0] as IndexWorkItem;

                if (mine != null)
                {
                    mine.FireComplete();
                }
            }

            List<IndexOperation> pendingDocuments = new List<IndexOperation>();
            object queueLock = (object)true;

            public void Update(Document doc, IndexWorkItem workItem)
            {
                Update(
                    new IndexOperation { Document = doc, Type = IndexOperationType.Save },
                    workItem
                    );
            }

            public void Update(IndexOperation operation, IndexWorkItem workItem)
            {
                operation.Document.Index = this.index;
                lock (queueLock)
                {
                    pendingDocuments.Add(operation);
                }
                Fire(workItem);
            }

            public void Update(IEnumerable<Document> batch, IndexWorkItem workItem)
            {
                var list = new List<IndexOperation>();

                foreach (var doc in batch)
                {
                    list.Add(new IndexOperation { Document = doc, Type = IndexOperationType.Save });
                }

                Update(list, workItem);
            }

            public void Update(IEnumerable<IndexOperation> batch, IndexWorkItem workItem)
            {
                batch.SetIndex(index);
                lock (queueLock)
                {
                    pendingDocuments.AddRange(batch);
                }
                Fire(workItem);
            }

            object indexControllerLock = (object)true;
            void Fire(IndexWorkItem workItem)
            {
                var wi = new ThreadPoolWorkItem("", true, false, 1, new ThreadStart(DoIndexing), workItem);
                pool.AddWorkItem(wi);
            }

            // This should never, ever run more than once at a time
            object indexingLock = (object)true;

            void DoIndexing()
            {
                bool hasChanged = false;
                lock (indexingLock)
                {
                    List<IndexOperation> items = null;
                    lock (queueLock)
                    {
                        if (pendingDocuments.Count == 0) return;
                        items = pendingDocuments;
                        pendingDocuments = new List<IndexOperation>();
                    }
                    if (items.Count > 0)
                    {
                        hasChanged = true;
                        IndexHelper.IndexDocuments(this.path, items);
                    }
                }
                if (hasChanged)
                {
                    FireChanged();
                }
            }

            #region IDisposable Members

            public void Dispose()
            {
                //throw new NotImplementedException();
            }

            #endregion
        }
    }
}
