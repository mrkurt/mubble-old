using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Mubble
{
    public class QueryBrokerMetrics
    {
        public int PendingReaderLocks = 0;
        public int PendingWriterLocks = 0;

        public int ReaderLockRequested()
        {
            return PendingReaderLocks++;
        }
        public int ReaderLockAcquired()
        {
            return PendingReaderLocks--;
        }

        public int WriterLockRequested()
        {
            return PendingWriterLocks++;
        }

        public int WriterLockAcquired()
        {
            return PendingWriterLocks--;
        }
    }
}
