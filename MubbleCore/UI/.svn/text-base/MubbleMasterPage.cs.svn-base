using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI
{
    /// <summary>
    /// Base class for Template Masterpages
    /// </summary>
    public class MasterPage : System.Web.UI.MasterPage
    {
        private Page page;
        /// <summary>
        /// Gets a reference to the Mubble.UI.Page instance that contains the control
        /// </summary>
        new public Page Page
        {
            get
            {
                if (page == null)
                {
                    page = base.Page as Page;
                }
                return page;
            }
        }
        public MasterPage()
            : base()
        {
        }
    }
}
