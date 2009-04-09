using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Mubble.Models.QueryEngine
{
    public class BooleanClause : QueryClause
    {
        private List<QueryClause> children = new List<QueryClause>();

        public List<QueryClause> Children
        {
            get { return children; }
        }

        public BooleanClause(bool isRequired, bool isExcluded)
            : base(isRequired, isExcluded) { }

        public BooleanClause(bool isRequired, bool isExcluded, params QueryClause[] clauses)
            : this(isRequired, isExcluded)
        {
            foreach (QueryClause clause in clauses)
            {
                this.Children.Add(clause);
            }
        }

        public void AddClause(QueryClause clause)
        {
            this.Children.Add(clause);
        }

        public override string ToString()
        {
            string temp = this.RequiredToString();
            temp += "(";
            for (int i = 0; i < this.Children.Count; i++)
            {
                if (i > 0) temp += " ";
                temp += this.Children[i].ToString();
            }
            temp += ")";
            temp += this.BoostToString();
            return temp;
        }

        public override int GetHashCode()
        {
            int hash = base.GetHashCode();

            foreach (QueryClause clause in this.Children)
            {
                hash ^= clause.GetHashCode();
            }
            return hash;
        }
    }
}
