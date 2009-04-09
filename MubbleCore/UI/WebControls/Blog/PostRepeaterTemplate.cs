using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;

namespace Mubble.UI.WebControls.Blog
{
    public class PostRepeaterTemplate : RepeaterTemplate
    {
        private Post post;
        public Post Post
        {
            get { return post; }
        }

        public override object DataItem
        {
            get
            {
                return base.DataItem;
            }
            set
            {
                if (value != null)
                {
                    post = value as Post;
                }
                base.DataItem = value;
            }
        }

        public PostRepeaterTemplate(int itemIndex, System.Web.UI.WebControls.ListItemType itemType) : base(itemIndex, itemType) { }
    }
}
