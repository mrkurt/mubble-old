using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Mubble.UI.WebControls
{
    [ParseChildren(false)]
    public class ContentPage : Mubble.UI.FeaturedContentControl
    {
        internal sealed class ViewOwner : System.Web.UI.WebControls.WebControl
        {

        }

        private ITemplate template;

        /// <summary>
        /// Gets or sets the template used for the control view
        /// </summary>
        [TemplateContainer(typeof(ContentPage))]
        public ITemplate Template
        {
            get { return template; }
            set { template = value; }
        }

        protected override void CreateChildControls()
        {
            if (this.Template != null && this.PageNumber > 0 && this.SelectedPage != null)
            {
                Controls.Clear();
                ContentPage.ViewOwner i = new ContentPage.ViewOwner();
                this.Template.InstantiateIn(i);
                Controls.Add(i);
            }
        }
    }
}
