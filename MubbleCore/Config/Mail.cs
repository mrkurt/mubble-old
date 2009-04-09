using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Config
{
    public class Mail : BaseSection<Mail>
    {
        private string staffDomain;

        public string StaffDomain
        {
            get { return staffDomain; }
            set { staffDomain = value; }
        }

    }
}
