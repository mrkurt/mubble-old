using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Mubble.UI.WebControls
{
    public class Randomizer : ContainerControl
    {
        private int showCount = 4;

        public int ShowCount
        {
            get { return showCount; }
            set { showCount = value; }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            ControlCollection children = this.Controls;

            List<System.Web.UI.Control> controls = new List<System.Web.UI.Control>();

            for (int i = children.Count - 1; i >= 0; i--)
            {
                if (!children[i].Visible)
                {
                    children.RemoveAt(i);
                    continue;
                }
                Conditionals.Conditional conditional = children[i] as Conditionals.Conditional;
                if (conditional != null && !conditional.IsTrue)
                {
                    children.RemoveAt(i);
                    continue;
                }
            }

            for (int i = 0; i < this.ShowCount && children.Count > 0; i++)
            {
                Random r = new Random();
                int index = r.Next(0,children.Count);
                controls.Add(children[index]);
                children.RemoveAt(index);
            }

            foreach (System.Web.UI.Control c in controls)
            {
                c.RenderControl(writer);
            }
        }
    }
}
