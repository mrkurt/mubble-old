using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;

namespace Mubble.UI.Conditionals
{
    public class Path : Conditional
    {
        private string startsWith;

        public string StartsWith
        {
            get { return startsWith; }
            set { startsWith = value; }
        }

        protected override bool Test()
        {
            bool passes = true;

            ILinkable link = Control.GetCurrentScope<ILinkable>(this);

            if (link == null) return false;

            string prefix = string.IsNullOrEmpty(this.StartsWith) ? "" : this.StartsWith;

            bool reverse = false;

            if (prefix.StartsWith("!"))
            {
                reverse = true;
                prefix = prefix.Substring(1);
            }

            if (!link.Url.Path.StartsWith(prefix))
            {
                passes = false;
            }
            return (!reverse && passes) || (!passes && reverse);
        }
    }
}
