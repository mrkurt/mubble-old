using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble
{
    class BackgroundWorker
    {
        public BackgroundWorker()
        {
            MiscUtil.Threading.CustomThreadPool pool = new MiscUtil.Threading.CustomThreadPool("background");
            
        }
    }
}
