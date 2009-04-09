using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.Indexing
{
    public class TermClause : QueryClause
    {
        public string Field { get; set; }
        public string Value { get; set; }

        public TermClauseType ValueType { get; set; }

        public TermClause()
            : base()
        {
            this.ValueType = TermClauseType.Term;
        }

        public override string ToString()
        {
            return string.Concat(
                this.TypeToString(),
                this.Field,
                ":",
                EscapeForLucene(this.Value),
                this.BoostToString()
                );
        }
    }

    public enum TermClauseType
    {
        Term = 1,
        Wildcard = 2,
        Fuzzy = 3
    }
}
