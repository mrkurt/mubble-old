using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Data
{
    public class IndexField : BaseQueryFilter
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string value;

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        private IndexFieldMode mode = IndexFieldMode.Default;

        public IndexFieldMode Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        public override Mubble.Models.QueryEngine.Query BuildQuery(Mubble.Models.QueryEngine.Query current)
        {
            if (this.Value != null && this.Name != null)
            {
					current.AddTerm(
						this.Name, 
						this.Value, 
						this.Value.Contains("*"),
						this.Mode == IndexFieldMode.Require, 
						this.Mode == IndexFieldMode.Exclude
						);
            }
            return current;
        }
    }
    public enum IndexFieldMode
    {
        Require,
        Exclude,
        Default
    }
}
