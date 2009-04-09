using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI
{
    public class AdminMasterPage : MasterPage
    {
        new public AdminPage Page
        {
            get
            {
                return base.Page as AdminPage;
            }
        }
    }
}
