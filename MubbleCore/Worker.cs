using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Mubble
{
    public delegate void WorkItemWithState(string state);

    public static class Worker
    {
        static MiscUtil.Threading.CustomThreadPool threadPool = new MiscUtil.Threading.CustomThreadPool("Background workers");

        static Worker()
        {
            threadPool.SetMinMaxThreads(5, 10);
            threadPool.WorkerThreadPriority = ThreadPriority.BelowNormal;
            threadPool.StartMinThreads();
        }

        public static void Queue(ThreadStart workItem)
        {
            threadPool.AddWorkItem(workItem);
        }

        public static int TotalThreads
        {
            get { return threadPool.TotalThreads; }
        }
        public static int MaxThreads
        {
            get { return threadPool.MaxThreads; }
        }
        public static int QueueLength
        {
            get { return threadPool.QueueLength; }
        }

        public static void Queue(WorkItemWithState workItem, string state)
        {
            threadPool.AddWorkItem(workItem, new object[] { state });
        }

        public static void Queue(Delegate workItem, params object[] parameters)
        {
            threadPool.AddWorkItem(workItem, parameters);
        }
    }
}
