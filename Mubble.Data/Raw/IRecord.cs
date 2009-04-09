using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.Data.Raw
{
    public interface IRecord
    {
        object this[string name] { get; set; }
    }
}
