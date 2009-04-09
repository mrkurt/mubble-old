using System;
using System.Collections.Generic;
using System.Text;

using Mubble.Models;

namespace Mubble.UI
{
    public class UserControl : System.Web.UI.UserControl
    {
        private Mubble.UI.Page page;

        /// <summary>
        /// Gets the container page for this control
        /// </summary>
        new public Mubble.UI.Page Page
        {
            get
            {
                if (this.page == null)
                {
                    this.page = base.Page as Mubble.UI.Page;
                }
                return page;
            }
        }

        /// <summary>
        /// Gets the content for the current request
        /// </summary>
        public Controller Content
        {
            get
            {
                return this.Page.Controller;
            }
        }

        /// <summary>
        /// Gets the parsed URL of the current request
        /// </summary>
        public virtual MubbleUrl Url
        {
            get
            {
                return this.Page.Url;
            }
            set { }
        }
	
    }
}
