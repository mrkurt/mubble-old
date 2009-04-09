using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models.Filters
{
    public class NormalizeStringFilterAttribute : ActiveObjects.FilterAttribute
    {

        public NormalizeStringFilterAttribute(string field) : base(field, ActiveObjects.FilterType.Set) { }

        public NormalizeStringFilterAttribute(string field, ActiveObjects.FilterType type) : base(field, type) { }

        public override string Filter(string input)
        {
            return String.Normalize(input);
        }
    }
}
