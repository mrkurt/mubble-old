using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Mubble.Models.QueryEngine
{
    public class TermClause : QueryClause
    {
        private TermClauseType type = TermClauseType.Term;

        public TermClauseType Type
        {
            get { return type; }
            set { type = value; }
        }


        private string field;

        public string Field
        {
            get { return field; }
            set { field = value; }
        }

        private string value;

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }


        public TermClause(string field, string value, bool isRequired, bool isExcluded)
            : base(isRequired, isExcluded)
        {
            this.Field = field;
            this.Value = value;
        }

        public TermClause(string field, string value, float boost)
            : base(false, false, boost)
        {
            this.Field = field;
            this.Value = value;
        }

        public TermClause(string field, string value, bool isWildCard, bool isRequired, bool isExcluded)
            : this(field, value, isRequired, isExcluded)
        {
            if (isWildCard) this.Type = TermClauseType.Wildcard;
        }

        public TermClause(string field, string value, TermClauseType type, bool isRequired, bool isExcluded)
            : this(field, value, isRequired, isExcluded)
        {
            this.Type = type;
        }

        public TermClause(string field, string value, TermClauseType type, float boost)
            : this(field, value, boost)
        {
            this.Type = type;
        }

        public override string ToString()
        {
            string temp = this.RequiredToString();
            temp += this.Field;
            temp += ":";
            temp += value;
            temp += this.BoostToString();
            return temp;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() ^ 
                this.Field.GetHashCode() ^ 
                this.Value.GetHashCode() ^ 
                this.Type.GetHashCode();
        }
    }

    public enum TermClauseType
    {
        Term = 1,
        Wildcard = 2,
        Fuzzy = 3
    }
}
