using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Config
{
    public class Benchmarks :BaseSection<Benchmarks>
    {
        private bool enabled = false;

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        private TimeSpan minTime = TimeSpan.FromMilliseconds(3000);

        public TimeSpan MinTime
        {
            get { return minTime; }
            set { minTime = value; }
        }

        private int queriesToTriggerLog = 20;

        public int QueriesToTriggerLog
        {
            get { return queriesToTriggerLog; }
            set { queriesToTriggerLog = value; }
        }

    }
}
