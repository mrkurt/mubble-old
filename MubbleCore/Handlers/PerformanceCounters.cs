using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Mubble.Handlers
{
    class PerformanceCounters : System.Web.IHttpModule
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region IHttpModule Members

        public void Dispose()
        {
            monitorCounters = false;
        }

        bool monitorCounters = true;
        public void Init(System.Web.HttpApplication context)
        {
            Thread t = new Thread(new ThreadStart(RefreshCounters));
            t.Start();

        }

        #endregion

        void RefreshCounters()
        {
            while (monitorCounters)
            {
                try
                {
                    LogAspThreads();
                    LogMubbleThreads();
                    Thread.Sleep(500);
                }
                catch(Exception ex)
                {
                    log.Error("Unabled to write performance counters, shutting thread down", ex);
                    monitorCounters = false;
                    return;
                }
            }
        }

        void LogAspThreads()
        {
            int availableWorker, availableIO;
            int maxWorker, maxIO;

            ThreadPool.GetAvailableThreads(out availableWorker, out availableIO);
            ThreadPool.GetMaxThreads(out maxWorker, out maxIO);

            PerformanceCounter c = new PerformanceCounter("Mubble", "Available ASP.NET Worker Threads");
            c.ReadOnly = false;
            c.RawValue = availableWorker;
            c.Close();

            c = new PerformanceCounter("Mubble", "Available ASP.NET IO Threads");
            c.ReadOnly = false;
            c.RawValue = availableIO;
            c.Close();

            c = new PerformanceCounter("Mubble", "Max ASP.NET Worker Threads");
            c.ReadOnly = false;
            c.RawValue = maxWorker;
            c.Close();

            c = new PerformanceCounter("Mubble", "Max ASP.NET IO Threads");
            c.ReadOnly = false;
            c.RawValue = maxIO;
            c.Close();
        }

        void LogMubbleThreads()
        {
            PerformanceCounter c = new PerformanceCounter("Mubble", "Available Mubble Worker Threads");
            c.ReadOnly = false;
            c.RawValue = Worker.MaxThreads - Worker.TotalThreads;
            c.Close();

            c = new PerformanceCounter("Mubble", "Max Mubble Worker Threads");
            c.ReadOnly = false;
            c.RawValue = Worker.MaxThreads;
            c.Close();

            c = new PerformanceCounter("Mubble", "Work items in Mubble queue");
            c.ReadOnly = false;
            c.RawValue = Worker.QueueLength;
            c.Close();
        }
    }
}
