using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using System.Text.RegularExpressions;

namespace Mubble.UI.Conditionals
{
    public class Link : Conditional
    {
        private string pathPattern;

        public string PathPattern
        {
            get { return pathPattern; }
            set { pathPattern = value; }
        }


        protected override bool Test()
        {
            if (string.IsNullOrEmpty(this.PathPattern)) return true;

            ILinkable linkable = Control.GetCurrentScope<ILinkable>(this);

            if (linkable == null) return false;

            return Regex.IsMatch(linkable.Url.Path, this.PathPattern);
        }
    }
}
