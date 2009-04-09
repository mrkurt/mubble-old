using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Mubble.UI.Conditionals
{
    public abstract class Conditional : ContainerControl
    {
        public Conditional() 
        {
            this.Load += new EventHandler(Conditional_Load);
        }

        void Conditional_Load(object sender, EventArgs e)
        {
            if (!this.IsTrue)
            {
                this.Controls.Clear();
            }
        }

        protected string conditions;

        /// <summary>
        /// Gets or sets a comma delimited list of conditions to test for
        /// </summary>
        [PersistenceMode(PersistenceMode.Attribute)]
        public string Is
        {
            get { return conditions; }
            set { conditions = value; }
        }

        private string useControl;

        /// <summary>
        /// Gets or sets the control this conditional should apply to
        /// </summary>
        public string UseControl
        {
            get { return useControl; }
            set { useControl = value; }
        }

        private bool? isTrue = null;

        public bool IsTrue
        {
            get 
            {
                if (isTrue == null)
                {
                    isTrue = this.Test();
                }
                return isTrue.Value; 
            }
        }


        protected abstract bool Test();
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (this.IsTrue)
            {
                base.Render(writer);
            }
        }

        protected T GetControl<T>() where T : class
        {
            if (this.UseControl != null)
            {
                return this.GetReferencedScope<T>();
            }
            else
            {
                return Mubble.UI.Control.GetCurrentScope<T>(this);
            }
        }

        protected T GetReferencedScope<T>() where T : class
        {
            System.Web.UI.Control c = GetReferencedScope(this.NamingContainer.Controls);
            if (c != null)
            {
                T value = Mubble.UI.Control.GetCurrentScope<T>(c);
                return value != null ? value : null;
            }
            return null;
        }

        protected System.Web.UI.Control GetReferencedScope(ControlCollection controls)
        {
            System.Web.UI.Control value = null;
            foreach (System.Web.UI.Control c in controls)
            {
                if (c.ID == this.UseControl)
                {
                    return c;
                }
                else
                {
                    value = GetReferencedScope(c.Controls);
                    if (value != null) return value;
                }
            }
            return null;
        }
    }
}
