using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Handlers
{
    public static class Settings
    {
        private static HandlerExtensions extensions = new HandlerExtensions();

        public static HandlerExtensions Extensions
        {
            get { return extensions; }
            set { extensions = value; }
        }

    }

    public class HandlerExtensions : System.Collections.Generic.Dictionary<string, string>
    {
        public HandlerExtensions() : base(StringComparer.CurrentCultureIgnoreCase) { }
        public string FindByKey(string key)
        {
            if (!base.ContainsKey(key))
            {
                int hitCount = 0;
                string lastKey = null;
                foreach (string k in base.Keys)
                {
                    if(key.Length > k.Length) continue;
                    if (k.Substring(k.Length - key.Length).Equals(key, StringComparison.CurrentCultureIgnoreCase))
                    {
                        hitCount++;
                        lastKey = k;
                    }
                }
                if (hitCount == 1 && lastKey != null)
                {
                    return base[lastKey];
                }
                else if(hitCount > 1)
                {
                    throw new KeyNotFoundException("The key partial matched multiple keys in the collection.");
                }
            }
            return base[key];
        }
    }
}
