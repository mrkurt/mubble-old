using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Config
{
    public class Index : BaseSection<Index>
    {
        private bool useRamSearcher = true;

        public bool UseRamSearcher
        {
            get { return useRamSearcher; }
            set { useRamSearcher = value; }
        }

        private bool enabled = true;

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public TimeSpan maxTimeForQueryRefresh = TimeSpan.FromSeconds(60);

        public TimeSpan MaxTimeForQueryRefresh
        {
            get { return maxTimeForQueryRefresh; }
            set { maxTimeForQueryRefresh = value; }
        }
    }
}
