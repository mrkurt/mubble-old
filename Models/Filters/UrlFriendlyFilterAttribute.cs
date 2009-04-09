using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models.Filters
{
    public class UrlFriendlyFilterAttribute : ActiveObjects.FilterAttribute
    {
        private bool lowerCase = false;

        public bool LowerCase
        {
            get { return lowerCase; }
            set { lowerCase = value; }
        }

        private bool allowPeriods = false;

        public bool AllowPeriods
        {
            get { return allowPeriods; }
            set { allowPeriods = value; }
        }


        public UrlFriendlyFilterAttribute(string field) : base(field, ActiveObjects.FilterType.Set) { }

        public UrlFriendlyFilterAttribute(string field, ActiveObjects.FilterType type) : base(field, type) { }

        public override string Filter(string input)
        {
            string output = String.UrlFriendly(input, this.AllowPeriods);
            return (lowerCase) ? output.ToLower() : output;
        }
    }
}
