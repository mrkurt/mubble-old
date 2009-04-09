using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;

namespace Mubble.UI
{
    public abstract class CompositeControl : System.Web.UI.WebControls.CompositeControl
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

        private Controller content;

        /// <summary>
        /// Gets the content for the current request
        /// </summary>
        public Controller Content
        {
            get
            {
                if (content == null)
                {
                    content = (this.Page != null) ?
                        this.Page.Controller : null;
                }
                return content;
            }
        }

        /// <summary>
        /// Gets the parsed URL of the current request
        /// </summary>
        public MubbleUrl Url
        {
            get
            {
                return this.Page.Url;
            }
        }
	
    }
}
