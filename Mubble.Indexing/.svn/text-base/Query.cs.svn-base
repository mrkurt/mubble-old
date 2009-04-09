using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.Indexing
{
    public class Query : BooleanClause
    {
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            var text = string.IsNullOrEmpty(this.Text) ? 
                base.ToString() : string.Concat(this.Text, " ", base.ToString());

            return base.ToString();
        }
    }
}