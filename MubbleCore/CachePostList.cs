using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using Mubble.Models;

namespace Mubble
{
    public class CachePostList : List<Guid>
    {
        private long totalResults;

        public long TotalResults
        {
            get { return totalResults; }
            set { totalResults = value; }
        }

        public CachePostList(ActiveCollection<Post> posts)
        {
            this.TotalResults = posts.TotalResults;
            foreach (Post p in posts)
            {
                this.Add(p.ID);
            }
        }

        public ActiveCollection<Post> GetFullPostList()
        {
            ActiveCollection<Post> posts = new ActiveCollection<Post>();
            posts.TotalResults = this.TotalResults;

            foreach (Guid id in this)
            {
                Post p = DataBroker.GetPost(id);
                if (p != null)
                {
                    posts.Add(p);
                }
            }

            return posts;
        }
    }
}
