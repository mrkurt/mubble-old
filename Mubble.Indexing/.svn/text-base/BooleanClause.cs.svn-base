using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.Indexing
{
    public class BooleanClause : QueryClause
    {
        public List<QueryClause> Children { get; set; }

        public BooleanClause()
        {
            this.Children = new List<QueryClause>();
        }

        public override string ToString()
        {
            string temp = this.TypeToString();
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
    }
}
