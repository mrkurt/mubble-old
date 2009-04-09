using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.Data
{
    public interface IData
    {
        object this[string field] { get; }
    }
}
