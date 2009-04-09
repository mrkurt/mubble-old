using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models.QueryEngine
{
    public class QueryClause
    {
        private bool isRequired;

        public bool IsRequired
        {
            get { return isRequired; }
            set { isRequired = value; }
        }

        private bool isExcluded;

        public bool IsExcluded
        {
            get { return isExcluded; }
            set { isExcluded = value; }
        }

        private float boost = 0;

        public float Boost
        {
            get { return boost; }
            set { boost = value; }
        }


        public QueryClause(bool isRequired, bool isExcluded)
        {
            this.IsRequired = isRequired;
            this.IsExcluded = isExcluded;
        }

        public QueryClause(bool isRequired, bool isExcluded, float boost)
            : this(isRequired, isExcluded)
        {
            this.Boost = boost;
        }

        protected string BoostToString()
        {
            if (this.Boost != 1 && this.Boost > 0)
            {
                return "^" + this.Boost.ToString();
            }
            return "";
        }

        protected string RequiredToString()
        {
            string temp = "";
            if (this.IsRequired) temp += "+";
            else if (this.IsExcluded) temp += "-";
            return temp;
        }

        public override int GetHashCode()
        {
            return this.IsRequired.GetHashCode() ^ this.IsExcluded.GetHashCode() ^ this.Boost.GetHashCode();
        }
    }
}
