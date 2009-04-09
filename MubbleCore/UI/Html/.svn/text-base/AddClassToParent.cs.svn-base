using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Html
{
    public class AddClassToParent : System.Web.UI.Control
    {
        private string className;

        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        public AddClassToParent()
        {
            this.Load += new EventHandler(AddClassToParent_Load);
        }

        void AddClassToParent_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlControl el = Control.GetCurrentScope<System.Web.UI.HtmlControls.HtmlControl>(this);
            string name = Control.GetReferencedValue(this, this.ClassName);
            string current = el.Attributes["class"];
            if (string.IsNullOrEmpty(current))
            {
                el.Attributes["class"] = name;
                return;
            }
            else
            {
                List<string> classes = new List<string>(current.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                if (!classes.Contains(name))
                {
                    classes.Add(name);
                    el.Attributes["class"] = string.Join(" ", classes.ToArray());
                }
            }
        }
    }
}
