using System;
using System.Collections.Generic;
using System.Text;
using Mubble.UI.Data;

namespace Mubble.UI.Conditionals
{
    public class ListHasItems : Conditional
    {
        private ScopeDirection listIs = ScopeDirection.Parent;

        public ScopeDirection ListIs
        {
            get { return listIs; }
            set { listIs = value; }
        }

	
        protected override bool Test()
        {
            List l = null;

            if (this.ListIs == ScopeDirection.Child)
            {
                l = Control.FindControl<List>(this);
            }
            else
            {
                l = Control.GetCurrentScope<List>(this);
            }
            if (l == null || l.Count <= 0) return false;
            else return true;
        }
    }

    public enum ScopeDirection
    {
        Parent = 1,
        Child = 2
    }
}
