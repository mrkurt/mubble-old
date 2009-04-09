using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI
{
    public class ConditionalView : ContainerControl
    {
        private string group;

        public string Group
        {
            get { return group; }
            set { group = value; }
        }

        private bool onlyOneInGroup = false;

        public bool OnlyOneInGroup
        {
            get { return onlyOneInGroup; }
            set { onlyOneInGroup = value; }
        }

        protected bool matches = true;

        public bool Matches
        {
            get { return matches; }
        }

        protected bool IsOtherControlInGroupVisible()
        {
            return this.IsOtherControlInGroupVisible(this.NamingContainer);
        }

        protected bool IsOtherControlInGroupVisible(System.Web.UI.Control control)
        {
            foreach (System.Web.UI.Control c in control.Controls)
            {
                if (c.Equals(this)) break;

                ConditionalView pc = c as ConditionalView;
                if (pc != null && pc.GetType() == this.GetType() && pc.Group == this.Group && pc.Matches)
                {
                    return true;
                }
                else
                {
                    return this.IsOtherControlInGroupVisible(c);
                }
            }
            return false;
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (matches && this.OnlyOneInGroup && this.IsOtherControlInGroupVisible())
            {
                matches = false;
            }
            if (this.Matches)
            {
                base.Render(writer);
            }
        }
    }
}
