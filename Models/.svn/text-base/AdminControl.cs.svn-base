using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{

	public partial class AdminControl
	{
        public string Key
        {
            get
            {
                int lastDot = this.FileName.LastIndexOf('.');
                return this.FileName.Substring(0, lastDot).ToLower();
            }
        }
	}
}
