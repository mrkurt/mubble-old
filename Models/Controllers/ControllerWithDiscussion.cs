using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models.Controllers
{
    [HasDiscussion(TitleField="Title", CloneFromField="Controller", DefaultStatus=DiscussionStatus.Inactive)]
    public abstract class ControllerWithDiscussion : Controller, IDiscussable
    {
        public Discussion Discussion
        {
            get { return this.DataManager.GetTypedProperty<Discussion>("Discussion"); }
            set { this.DataManager["Discussion"] = value; }
        }

        public virtual CommentCollection GetComments(int page, Dictionary<string, string> parameters)
        {
            return this.Discussion.GetComments(page);
        }
    }
}
