using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Data
{
    public interface IFilter
    {
        IControl Parent { get; set; }
        void Before(Dictionary<string, object> parameters);
    }
}
