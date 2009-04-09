using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Mubble.UI.Conditionals
{
    public class Post : Conditional
    {
        private Mubble.Models.Post currentPost;
        protected Mubble.Models.Post CurrentPost
        {
            get 
            {
                if (currentPost == null && this.UseControl == null)
                {
                    currentPost = Control.GetCurrentScope<Mubble.Models.Post>(this);
                }
                if (currentPost == null 
                    && this.UseControl != null
                    && this.PostsControl != null 
                    && this.PostsControl.PostCollection != null 
                    && this.PostsControl.PostCollection.Count > 0)
                {
                    currentPost = this.PostsControl.PostCollection[0];
                }
                return currentPost; 
            }
        }

        private Mubble.UI.Data.Posts postsControl;

        protected Mubble.UI.Data.Posts PostsControl
        {
            get { if (postsControl == null) postsControl = this.GetControl<Mubble.UI.Data.Posts>(); return postsControl; }
        }

        
        protected override bool Test()
        {
            conditions = (conditions == null) ? "" : conditions;
            return this.Test(conditions.Split(new char[]{','}));
        }

        protected bool Test(params string[] tests)
        {
            if (this.CurrentPost == null) return false;
            foreach (string test in tests)
            {
                bool negative = (test.Length > 0 && test[0] == '!');
                string pattern = (negative) ? test.Substring(1) : test;
                bool fails = false;
                switch (pattern.ToLower())
                {
                    case "singlepost":
                        fails = (this.PostsControl.PostCollection.Count != 1);
                        break;
                    case "first":
                        fails = (this.PostsControl.PostCollection.Count < 1 || this.PostsControl.PostCollection[0].ID != this.CurrentPost.ID);
                        break;
                    case "last":
                        fails = (this.PostsControl.PostCollection.Count > PostsControl.PostCollection.IndexOf(this.CurrentPost) || 
                            this.PostsControl.PostCollection[postsControl.PostCollection.Count - 1].ID != this.CurrentPost.ID); ;
                        break;
                    case "newdate":
                        int currentIndex = this.PostsControl.PostCollection.IndexOf(this.CurrentPost);
                        if(currentIndex == 0) break;
                        DateTime current = this.CurrentPost.PublishDate.Date;
                        fails = (current == this.PostsControl.PostCollection[currentIndex - 1].PublishDate.Date);
                        break;
                    case "extendedpost":
                        fails = (this.CurrentPost.Body.IndexOf("<more />") < 0 || this.CurrentPost.Body.Trim().EndsWith("<more />"));
                        break;
                    case "discussionexists":
                        fails = (this.CurrentPost.Discussion.Status != Mubble.Models.DiscussionStatus.Exists);
                        break;
                }
                if ((fails && !negative) || (!fails && negative)) return false;
            }
            return true;
        }
    }
}
