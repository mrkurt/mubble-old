using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;

namespace Mubble.UI.Admin
{
    public class ActiveObjectFieldEditor : UserControl
    {
        private IActiveObject activeObject;

        public virtual IActiveObject ActiveObject
        {
            get { return activeObject; }
            set { activeObject = value; }
        }

        private string field;

        public virtual string Field
        {
            get { return field; }
            set { field = value; }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public static void BindActiveObjectEditors(System.Web.UI.Control control, IActiveObject contentObject)
        {
            foreach (System.Web.UI.Control c in control.Controls)
            {
                ActiveObjectFieldEditor editor = c as ActiveObjectFieldEditor;
                if (editor != null)
                {
                    editor.ActiveObject = contentObject;
                }
                else
                {
                    BindActiveObjectEditors(c, contentObject);
                }
            }
        }
    }
}
