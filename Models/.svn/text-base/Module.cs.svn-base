using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{

	public partial class Module
	{
        public ModuleControl FindControl(string control)
        {
            foreach (ModuleControl c in this.ModuleControls)
            {
                if (control.Equals(c.FileName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return c;
                }
            }
            return null;
        }
        public string GetControlType(string control)
        {
            ModuleControl c = this.FindControl(control);
            return (c == null) ? null : c.Type;
        }
    }
}
