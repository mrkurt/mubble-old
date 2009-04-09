using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble
{
    public class PathParameters : Dictionary<string, string>
    {
        public new string this[string key]
        {
            get
            {
                string value = null;
                base.TryGetValue(key, out value);
                return value;
            }
            set { base[key] = value; }
        }
    }
}
