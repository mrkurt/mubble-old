using System;
using System.Collections.Generic;
using System.Text;

using Mubble.Models;
using ActiveObjects;
using System.Web.Caching;
using System.Text.RegularExpressions;

namespace Mubble.UI.WebControls.Blog
{
    /// <summary>
    /// A server control used to show a list of blog posts
    /// </summary>
    public class Posts : Mubble.UI.Repeater, IPageable, IActiveObjectContainer
    {
        #region Paging stuff
        /// <summary>
        /// Gets the link format for the current list
        /// </summary>
        public string PageLinkFormat
        {
            get
            {
                string url = Regex.Replace(Page.CurrentUrl, @"\/p\d+", "").Replace("/m-blog", "") + "/p{0}";
                if (Params["Year"] != null && Params["Month"] != null && Params["Day"] != null)
                {
                    url = Page.CurrentUrl +
                        string.Format("/{0}/{1}/{2}/{3}",
                        Params["Year"],
                        Params["Month"],
                        Params["Day"],
                        "p{0}"
                        );
                }
                else if (Params["Year"] != null && Params["Month"] != null)
                {
                    url = Page.CurrentUrl +
                        string.Format("/{0}/{1}/{2}",
                        Params["Year"],
                        Params["Month"],
                        "p{0}"
                        );
                }
                else if (Params["Year"] != null)
                {
                    url = Page.CurrentUrl +
                        string.Format("/{0}/{1}",
                        Params["Year"],
                        "p{0}"
                        );
                }
                return url;
            }
        }
        private int pageCount;

        /// <summary>
        /// Gets the total number of pages for this control
        /// </summary>
        public int PageCount
        {
            get
            {
                this.EnsurePages();
                return pageCount;
            }
        }

        private int pageNumber = 0;

        /// <summary>
        /// Gets or sets the page number to display.
        /// </summary>
        public int PageNumber
        {
            get
            {
                if (pageNumber <= 0)
                {
                    this.EnsurePages();
                }
                return pageNumber;
            }
            set { pageNumber = value; }
        }

        public PagePair CurrentPage
        {
            get
            {
                PagePair p = new PagePair();
                p.Link = string.Format(this.PageLinkFormat, this.PageNumber);
                p.Name = this.PageNumber.ToString();
                return p;
            }
        }

        public PagePair NextPage
        {
            get
            {
                PagePair p = null;
                if (this.Mode == PostsDisplayMode.Multiple)
                {
                    int newPage = this.PageNumber + 1;
                    if (newPage <= this.PageCount)
                    {
                        p = new PagePair();
                        p.Link = string.Format(this.PageLinkFormat, newPage);
                        p.Name = newPage.ToString();
                    }
                }
                else if (this.SinglePost != null && this.SinglePost.NextPost != null)
                {
                    p = new PagePair();
                    p.Link = this.Url.ToString(this.Url.Handler, this.SinglePost.NextPost.Url.Extra);
                    p.Name = this.SinglePost.NextPost.Title;
                }
                return p;
            }
        }

        public PagePair PreviousPage
        {
            get
            {
                PagePair p = null;
                if (this.Mode == PostsDisplayMode.Multiple)
                {
                    int newPage = this.PageNumber - 1;
                    if (newPage >= 1)
                    {
                        p = new PagePair();
                        p.Link = string.Format(this.PageLinkFormat, newPage);
                        p.Name = newPage.ToString();
                    }
                }
                else if (this.SinglePost != null && this.SinglePost.PreviousPost != null)
                {
                    p = new PagePair();
                    p.Link = this.Url.ToString(this.Url.Handler, this.SinglePost.PreviousPost.Url.Extra);
                    p.Name = this.SinglePost.PreviousPost.Title;
                }
                return p;
            }
        }

        public List<PagePair> AllPages
        {
            get
            {
                List<PagePair> pages = new List<PagePair>();
                for (int i = 1; i <= this.PageCount; i++)
                {
                    PagePair p = new PagePair();
                    p.Link = string.Format(this.PageLinkFormat, i);
                    p.Name = i.ToString();
                    pages.Add(p);
                }
                return pages;
            }
        }

        private int pageSize = 40;

        /// <summary>
        /// Gets or sets the size of pages to use with this listing
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public bool IsPageable { get { return this.Mode == PostsDisplayMode.Multiple; } }

        #endregion

        private bool ignoreUrlParameters = false;

        /// <summary>
        /// Gets or sets a flag indicating whether the listing should ignore passed in URL parameters for filtering and sorting.
        /// This is useful when the list is used as a side item rather than the main portion of a page, and the parameters are meant
        /// for a different control.
        /// </summary>
        public bool IgnoreUrlParameters
        {
            get { return ignoreUrlParameters; }
            set { ignoreUrlParameters = value; }
        }

        private PostsDisplayMode mode;

        public PostsDisplayMode Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        private Post singlePost;

        public Post SinglePost
        {
            get { return singlePost; }
        }

        public IActiveObject ActiveObject { get { return this.SinglePost; } }

        /// <summary>
        /// Sets the DataSource to this page's posts, then databinds.
        /// </summary>
        public override void DataBind()
        {
            this.EnsurePosts();
            base.DataBind();
        }

        /// <summary>
        /// Ensures that all the page data (even that derived from the underlying list) is accessible.
        /// </summary>
        public void EnsurePages()
        {
            this.EnsurePosts();
        }



        /// <summary>
        /// Ensures that the list of posts has been set.
        /// </summary>
        public void EnsurePosts()
        {
            if (this.DataSource == null)
            {
                Dictionary<string, object> parameters = this.BuildQueryParameters();

                int page = 1;
                if (!this.IgnoreUrlParameters && Params["Page"] != null)
                {
                    int.TryParse(Params["Page"], out page);
                }
                if (this.pageNumber == 0) this.PageNumber = page;

                int start = (this.PageNumber - 1) * pageSize;
                if (start < 0) start = 0;
                int end = start + pageSize;

                parameters.Add("RowIndex_start", start);
                parameters.Add("RowIndex_end", end);

                ActiveCollection<Post> posts = Post.Find(parameters);

                if (this.Mode == PostsDisplayMode.Single && posts.Count == 1)
                {
                    this.singlePost = posts[0];
                    Page.SetCacheDependency(this.Content);
                }

                Page.SetCacheDependency(this.Content);

                this.DataSource = posts;
                this.pageCount = posts.PageCount(this.PageSize);
            }
        }

        protected Dictionary<string, object> BuildQueryParameters()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);

            parameters.Add("ControllerID", this.Content.ID);

            if (Params["Slug"] != null)
            {
                this.Mode = PostsDisplayMode.Single;
                parameters.Add("Slug", Params["Slug"]);
            }
            else
            {
                this.Mode = PostsDisplayMode.Multiple;
            }

            if (!this.IgnoreUrlParameters && this.Mode == PostsDisplayMode.Multiple)
            {
                this.AddDateRangeFilters(parameters);
            }

            if (!Security.User.HasPermission(this.Content, PermissionType.Create, PermissionType.Edit))
            {
                parameters.Add("Status", PublishStatus.Published);
                if (!parameters.ContainsKey("StartPublishDate"))
                {
                    parameters.Add("StartPublishDate", new DateTime(1970, 1, 1));
                }
                if (parameters.ContainsKey("EndPublishDate"))
                {
                    parameters["EndPublishDate"] = DateTime.Now;
                }
                else
                {
                    parameters.Add("EndPublishDate", DateTime.Now);
                }
            }

            return parameters;
        }

        protected void AddDateRangeFilters(Dictionary<string, object> parameters)
        {
            DateTime start = new DateTime(1970, 1, 1), end = DateTime.MaxValue;
            if (Params["Year"] != null && Params["Month"] != null && Params["Day"] != null)
            {
                start = DateTime.Parse(
                    string.Format("{0}/{1}/{2} 00:00:00",
                        Params["Month"],
                        Params["Day"],
                        Params["Year"]
                        )
                    );
                end = start.AddDays(1);
            }
            else if (Params["Year"] != null && Params["Month"] != null)
            {
                start = DateTime.Parse(
                    string.Format("{0}/1/{1} 00:00:00",
                        Params["Month"],
                        Params["Year"]
                        )
                    );
                end = start.AddMonths(1);
            }
            else if (Params["Year"] != null)
            {
                start = DateTime.Parse(
                    string.Format("1/1/{0} 00:00:00",
                        Params["Year"]
                        )
                    );
                end = start.AddYears(1);
            }
            parameters.Add("StartPublishDate", start);
            parameters.Add("EndPublishDate", end);
        }

        protected override System.Web.UI.WebControls.RepeaterItem CreateItem(int itemIndex, System.Web.UI.WebControls.ListItemType itemType)
        {
            PostRepeaterTemplate pt = new PostRepeaterTemplate(itemIndex, itemType);
            pt.Controller = this.Content;
            return pt;
        }
    }

    public enum PostsDisplayMode
    {
        Single = 1,
        Multiple = 2
    }
}
