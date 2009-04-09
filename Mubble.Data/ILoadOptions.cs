using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Mubble.Data
{
    internal interface ILoadOptions
    {
        DataLoadOptions GetLoadOptions();
    }
}
