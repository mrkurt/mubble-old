using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{

	public partial class ModuleControl
	{

        public AdminControl DefaultAdminControl
        {
            get
            {
                foreach (AdminControl control in AdminControls)
                {
                    if (control.IsDefault)
                    {
                        return control;
                    }
                }
                if (AdminControls.Count > 0)
                {
                    return AdminControls[0];
                }
                return null;
            }
        }

        /// <summary>
        /// Finds the specified admin control using its filename
        /// </summary>
        /// <param name="fileName">fileName of the admin control to find</param>
        /// <returns>The admin control.  Null if it does not exist</returns>
        public AdminControl Find(string fileName)
        {
            if (this.AdminControls == null)
            {
                return null;
            }
            foreach (AdminControl control in this.AdminControls)
            {
                if (control.FileName.ToLower() == fileName.ToLower())
                {
                    return control;
                }
            }
            return null;
        }	
	}
}
