using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble
{
    public class Benchmark
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private DateTime startTime;

        public DateTime StartTime
        {
            get { return startTime; }
        }

        private DateTime endTime;

        public DateTime EndTime
        {
            get { return endTime; }
        }

        public TimeSpan ElapsedTime { get { return endTime - startTime; } }

        public void Start()
        {
            this.startTime = DateTime.Now;
        }

        public void End()
        {
            this.endTime = DateTime.Now;
        }

        public void Log(string message)
        {
            log.InfoFormat("{0} took {1}", message, this.ElapsedTime);
        }

        public Benchmark(bool autoStart)
        {
            if (autoStart) this.Start();
        }

        public Benchmark() : this(false) { }
    }
}
