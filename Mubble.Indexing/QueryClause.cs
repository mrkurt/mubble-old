using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.Indexing
{
    public abstract class QueryClause
    {
        public QueryClauseType Type { get; set; }
        public float Boost { get; set; }

        protected string TypeToString()
        {
            if (this.Type == QueryClauseType.Excluded)
            {
                return "-";
            }
            else if (this.Type == QueryClauseType.Required)
            {
                return "+";
            }
            else
            {
                return "";
            }
        }

        static string[] toEscape = "+ - && || ! ( ) { } [ ] ^ \" ~ * ? : \\".Split(' ');
        protected static string EscapeForLucene(string value)
        {
            // + - && || ! ( ) { } [ ] ^ " ~ * ? : \
            foreach (var s in toEscape)
            {
                value = value.Replace(s, "\\" + s);
            }
            return value.Contains(" ") ? string.Concat("\"", value, "\"") : value;
        }

        protected string BoostToString()
        {
            if (this.Boost != 1 && this.Boost > 0)
            {
                return string.Concat("^",this.Boost.ToString());
            }
            return "";
        }
    }
}
