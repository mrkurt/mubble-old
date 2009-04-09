using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble
{
    public class CachePointer
    {
        private string newKey;

        public string NewKey
        {
            get { return newKey; }
        }

        public CachePointer(string newKey)
        {
            this.newKey = newKey;
        }

        public override string ToString()
        {
            return string.Concat(base.ToString(), ":", this.newKey);
        }
    }
}
