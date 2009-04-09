using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models
{
    public class LinkParameters
    {
        private Dictionary<string, List<object>> values = new Dictionary<string, List<object>>(StringComparer.CurrentCultureIgnoreCase);
        
        public object this[string key]
        {
            get { return this.values.ContainsKey(key) ? this.values[key] : new List<object>(); }
            set { this[key, true] = value; }
        }
        public object this[string key, bool reset]
        {
            set
            {
                List<object> values = (reset) ? new List<object>() : (List<object>)this[key];
                values.Add(value);
                if (this.values.ContainsKey(key))
                {
                    this.values[key] = values;
                }
                else
                {
                    this.values.Add(key, values);
                }
            }
        }

        public bool HasItems { get { return this.values.Count > 0; } }

        public Dictionary<string, List<object>>.KeyCollection Keys
        {
            get { return values.Keys; }
        }
    }
}
