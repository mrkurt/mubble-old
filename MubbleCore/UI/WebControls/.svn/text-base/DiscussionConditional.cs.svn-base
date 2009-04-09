using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;

namespace Mubble.UI.WebControls
{
    public class DiscussionConditional : ConditionalView
    {
        private bool discussionExistsOnly = false;

        public bool DiscussionExistsOnly
        {
            get { return discussionExistsOnly; }
            set { discussionExistsOnly = value; }
        }

        private Discussion discussion;

        public Discussion Discussion
        {
            get { return discussion; }
            set { discussion = value; }
        }

        public override void DataBind()
        {
            if (this.Visible) base.DataBind();
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (this.Discussion == null)
            {
                this.Visible = false;
                return;
            }
            if (this.DiscussionExistsOnly && this.Discussion.Status != DiscussionStatus.Exists)
            {
                this.Visible = false;
                return;
            }
            if (this.Visible)
            {
                base.Render(writer);
            }
        }
    }
}
