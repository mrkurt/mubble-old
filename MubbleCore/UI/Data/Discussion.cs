using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using Mubble.Models;

namespace Mubble.UI.Data
{
    public class Discussion : ContainerControl, IScoped
    {
        #region IScoped Members
        private Mubble.Models.Discussion discussion;
        public object Scope
        {
            get 
            {
                this.LoadDiscussion();
                return discussion;
            }
        }
        #endregion

        public Discussion()
        {
            this.Load += new EventHandler(Discussion_Load);
        }

        void Discussion_Load(object sender, EventArgs e)
        {
            this.LoadDiscussion();
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            this.LoadDiscussion();
            if (discussion != null && discussion.Status == DiscussionStatus.Exists)
            {
                base.Render(writer);
            }
        }

        bool discussionLoaded = false;
        private void LoadDiscussion()
        {
            if (discussionLoaded) return;
            discussionLoaded = true;

            IDiscussable container = Control.GetCurrentScope<IDiscussable>(this.Parent);
            if (container != null && container.Discussion != null)
            {
                discussion = container.Discussion;
            }
        }
    }
}
