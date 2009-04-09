using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models
{
    public class RouteParameters : Dictionary<string, object>
    {
        new public object this[string name]
        {
            get
            {
                if (base.ContainsKey(name))
                {
                    return base[name];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (base.ContainsKey(name))
                {
                    base[name] = value;
                }
                else
                {
                    base.Add(name, value);
                }
            }
        }

        public RouteParameters()
        {

        }

        public RouteParameters(params object[][] parameters) : this()
        {
            foreach (object[] rp in parameters)
            {
                if (rp.Length == 2 && rp[0] != null)
                {
                    this[rp[0].ToString()] = rp[1];
                }
            }
        }

        public object Get(string name, object defaultValue)
        {
            if (this.ContainsKey(name))
            {
                return this[name];
            }
            return defaultValue;
        }
    }
}
