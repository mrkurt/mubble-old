using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mubble.Indexing
{
    public delegate void IndexWorkItemComplete();
    public delegate void IndexWorkItemError(Exception exception, ref bool handled);
    public enum IndexWorkItemStatus
    {
        Pending = 1,
        Running = 2,
        Error = 3,
        Complete = 4
    }
    public class IndexWorkItem
    {
        public IndexWorkItemComplete OnComplete { get; private set; }
        public IndexWorkItemError OnError { get; private set; }
        public Exception Exception { get; private set; }

        public IndexWorkItemStatus Status { get; private set; }

        private EventWaitHandle waitHandle;

        internal void FireBeforeRun()
        {
            this.Status = IndexWorkItemStatus.Running;
        }

        internal void FireComplete()
        {
            try
            {
                this.Status = IndexWorkItemStatus.Complete;
                if (this.OnComplete != null)
                {
                    this.OnComplete();
                }
            }
            finally
            {
                this.waitHandle.Set();
            }
        }

        internal void FireError(Exception ex, ref bool handled)
        {
            try
            {
                this.Status = IndexWorkItemStatus.Error;
                if (this.OnError != null)
                {
                    this.Exception = ex;
                    this.OnError(ex, ref handled);
                }
            }
            finally
            {
                this.waitHandle.Set();
            }
        }

        internal IndexWorkItem() : this(null, null)
        {

        }

        public IndexWorkItem(IndexWorkItemComplete onComplete, IndexWorkItemError onError)
        {
            this.waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
            this.Status = IndexWorkItemStatus.Pending;
            this.OnComplete = onComplete;
            this.OnError = onError;
        }

        public bool WaitOne()
        {
            return this.waitHandle.WaitOne();
        }

        public IndexWorkItem(IndexWorkItemError onError) : this(null, onError) { }

        public IndexWorkItem(IndexWorkItemComplete onComplete) : this(onComplete, null) { }
    }
}
