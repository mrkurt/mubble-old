using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Conditionals
{
    public class Group : Conditional
    {

        private bool? childMatch = null;
        private int trueIndex = -1;
        protected override bool Test()
        {
            if (childMatch == null)
            {
                childMatch = false;
                System.Web.UI.Control c = null;
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    Conditional conditional = this.Controls[i] as Conditional;
                    if (conditional == null || conditional.IsTrue)
                    {
                        trueIndex = i;
                        childMatch = true;
                        c = this.Controls[i];
                        break;
                    }
                }
                this.Controls.Clear();
                if (c != null)
                {
                    this.Controls.Add(c);
                }
            }
            return childMatch.Value;
        }

        public Group()
        {
            this.Load += new EventHandler(Group_Load);
        }

        void Group_Load(object sender, EventArgs e)
        {
            this.Test();
        }
    }
}
