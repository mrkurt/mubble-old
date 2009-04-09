using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;

namespace Mubble.UI.WebControls.Blog
{
    public class ItemConditional : ContainerControl
    {
        private bool newDayOnly = false;

        public bool NewDayOnly
        {
            get { return newDayOnly; }
            set { newDayOnly = value; }
        }

        private bool multiplePosts = false;

        public bool MultiplePosts
        {
            get { return multiplePosts; }
            set { multiplePosts = value; }
        }

        private bool singlePost = false;

        public bool SinglePost
        {
            get { return singlePost; }
            set { singlePost = value; }
        }

        private bool hasExtendedBody = false;

        public bool HasExtendedBody
        {
            get { return hasExtendedBody; }
            set { hasExtendedBody = value; }
        }

        private bool isFirstPost = false;

        public bool IsFirstPost
        {
            get { return isFirstPost; }
            set { isFirstPost = value; }
        }

        private bool isNotFirstPost = false;

        public bool IsNotFirstPost
        {
            get { return isNotFirstPost; }
            set { isNotFirstPost = value; }
        }


        private string postsControl;

        public string PostsControl
        {
            get { return postsControl; }
            set { postsControl = value; }
        }

        protected bool IsNewDay
        {
            get
            {
                if (this.AppliesTo == null) return true;
                if (this.Parent is PostRepeaterTemplate)
                {
                    if (this.AppliesTo.Items.Count == 0) return true;
                    PostRepeaterTemplate lastPost = this.AppliesTo.Items[this.AppliesTo.Items.Count - 1] as PostRepeaterTemplate;
                    if (lastPost.Post == null) return true;
                    Post last = this.CurrentPost;


                    DateTime current = last.PublishDate;

                    return current.Date != lastPost.Post.PublishDate.Date;
                }
                else
                {
                    return true;
                }
            }
        }

        protected Post CurrentPost
        {
            get
            {
                if (this.Parent is PostRepeaterTemplate)
                {
                    return ((PostRepeaterTemplate)this.Parent).Post;
                }
                return null;
            }
        }

        private Posts _appliesTo;
        protected Posts AppliesTo
        {
            get
            {
                if (_appliesTo == null)
                {
                    if (this.PostsControl == null)
                    {
                        _appliesTo = this.GetParentPostsControl(this);
                    }
                    else
                    {
                        _appliesTo = this.GetReferencedPostsControl();
                    }
                }
                return _appliesTo;
            }
        }

        public override void DataBind()
        {
            if (this.AppliesTo == null)
            {
                this.Visible = false;
                return;
            }
            if (this.NewDayOnly && !this.IsNewDay)
            {
                this.Visible = false;
            }
            if (this.MultiplePosts && this.AppliesTo.Mode != PostsDisplayMode.Multiple)
            {
                this.Visible = false;
            }
            if (this.SinglePost && this.AppliesTo.Mode != PostsDisplayMode.Single)
            {
                this.Visible = false;
            }
            if (this.HasExtendedBody && (this.CurrentPost.BodyExtended == null || this.CurrentPost.BodyExtended.Trim().Length == 0))
            {
                this.Visible = false;
            }
            if (this.IsFirstPost && this.AppliesTo.Items.Count > 0)
            {
                this.Visible = false;
            }
            if (this.IsNotFirstPost && this.AppliesTo.Items.Count == 0)
            {
                this.Visible = false;
            }
            if (this.Visible) base.DataBind();
        }

        protected Posts GetParentPostsControl(System.Web.UI.Control current)
        {
            if (current is Posts) return (Posts)current;
            if (current == null) return null;
            return this.GetParentPostsControl(current.Parent);
        }

        protected Posts GetReferencedPostsControl()
        {
            foreach (System.Web.UI.Control c in this.NamingContainer.Controls)
            {
                if (c.ID == this.PostsControl)
                {
                    return c as Posts;
                }
            }
            return null;
        }
    }
}
