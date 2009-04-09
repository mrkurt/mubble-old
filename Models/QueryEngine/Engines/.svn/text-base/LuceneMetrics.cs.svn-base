using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models.QueryEngine.Engines
{
    public class LuceneMetrics
    {
        public int TotalSearchersOpened = 0;
        public int CurrentSearchersOpened = 0;

        public int SearchesRun = 0;

        public void SearcherOpened()
        {
            TotalSearchersOpened++;
            CurrentSearchersOpened++;
        }

        public void SearcherClosed()
        {
            CurrentSearchersOpened--;
        }
    }
}
