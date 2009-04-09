using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Mubble.Models;
using Mubble.Models.Controllers;
using ActiveObjects;

namespace Mubble.UI.Data
{
    public class Posts : Mubble.UI.BlankRepeater, IPaging
    {
        protected ActiveCollection<Mubble.Models.Post> posts;
        public ActiveCollection<Mubble.Models.Post> PostCollection { get { this.EnsurePosts();  return posts; } }

        private List<IPostFilter> filters = new List<IPostFilter>();

        private PageNumber pager = new PageNumber();

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<IPostFilter> Filters
        {
            get { return filters; }
            set { filters = value; }
        }

        private bool setPageTitle = true;

        public bool SetPageTitle
        {
            get { return setPageTitle; }
            set { setPageTitle = value; }
        }

        public Posts()
        {
            this.Load += new EventHandler(Posts_Load);
        }

        void Posts_Load(object sender, EventArgs e)
        {
            this.EnsurePosts();
        }

        #region Filtering
        /// <summary>
        /// Gets the current working set of filters
        /// </summary>
        protected List<IPostFilter> GetFilters()
        {
            int startIndex = 0;
            List<IPostFilter> set = new List<IPostFilter>(this.Filters);
            set.Insert(0, new ScopeFilter());
            for (int i = 0; i < set.Count; i++)
            {
                set[i].Parent = this;
                if (set[i] is ClearFilters) startIndex = i;
            }
            return set.GetRange(startIndex, set.Count - startIndex);
        }
        protected void GetPosts()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            foreach (IFilter filter in this.GetFilters())
            {
                if (filter is PageNumber) pager = (PageNumber)filter;
                this.ApplyFilter(filter, parameters);
            }
            this.ApplySecurityFilter(parameters);

            ActiveCollection<Post> posts = null;
            if (parameters.ContainsKey("Slug") && parameters.ContainsKey("ControllerID"))
            {
                Post p = DataBroker.GetPost((Guid)parameters["ControllerID"], (string)parameters["Slug"]);
                posts = new ActiveCollection<Post>();
                if (
                    p == null ||
                    (parameters.ContainsKey("Status") && !p.Status.Equals(parameters["Status"])) ||
                    (parameters.ContainsKey("EndPublishDate") && p.PublishDate.CompareTo(parameters["EndPublishDate"]) > 0)
                    )
                {
                    posts.TotalResults = 0;
                }
                else if(p != null)
                {
                    posts.TotalResults = 1;
                    posts.Add(p);
                    this.Page.SetCacheDependency(this.Content);
                }
            }
            else
            {
                posts = DataBroker.GetPosts(parameters);
            }

            this.posts = posts;            
            this.DataSource = this.posts;
            base.DataBind();
        }
        protected void ApplySecurityFilter(Dictionary<string, object> parameters)
        {
            if (!Security.User.HasPermission(this.Content, PermissionType.Create, PermissionType.Edit)
                && !Security.User.HasFlag(this.Content, "view unpublished")
                )
            {
                if (!parameters.ContainsKey("Status"))
                {
                    parameters.Add("Status", PublishStatus.Published);
                }
                else
                {
                    parameters["Status"] = PublishStatus.Published;
                }
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
        }
        protected void ApplyFilter(IFilter filter, Dictionary<string, object> parameters)
        {
            ScopeFilter sf = filter as ScopeFilter;
            if (sf != null)
            {
                Blog blog = Control.GetCurrentScope<Blog>(this);
                if (blog != null && !parameters.ContainsKey("ControllerID"))
                {
                    parameters.Add("ControllerID", blog.ID);
                    
                    if (filter is ScopeFilter)
                    {
                        this.Page.SetCacheDependency(blog);
                    }
                }
            }
            else
            {
                filter.Before(parameters);
            }
        }
        #endregion

        #region Templates
        [TemplateContainer(typeof(PostRepeaterTemplate)), PersistenceMode(PersistenceMode.InnerProperty), Browsable(false)]
        public System.Web.UI.ITemplate PostTemplate
        {
            get { return this.ItemTemplate; }
            set { this.ItemTemplate = value; }
        }

        protected override System.Web.UI.WebControls.RepeaterItem CreateItem(int itemIndex, System.Web.UI.WebControls.ListItemType itemType)
        {
            PostRepeaterTemplate item = new PostRepeaterTemplate(itemIndex, itemType);
            item.Controller = this.Content;
            return item;
        }

        /// <summary>
        /// Gets or sets the location of the page template file
        /// </summary>
        public string PostTemplatePath
        {
            get { return this.ItemTemplatePath; }
            set { this.ItemTemplatePath = value; }
        }
        #endregion

        

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            this.EnsurePosts();
            if (this.PostCollection.Count == 1)
            {
                if (this.SetPageTitle)
                {
                    this.Page.SetPageTitle(this.PostCollection[0].Title);
                }
            }
            if (this.Items.Count > 0)
            {
                base.Render(writer);
            }
        }


        #region IPaging Members and helpers

        protected void EnsurePosts()
        {
            if (posts == null)
            {
                this.GetPosts();
            }
        }

        public List<Link> PageLinks
        {
            get
            {
                this.EnsurePosts();
                List<Link> ps = new List<Link>();
                if (this.posts != null)
                {
                    if (this.posts.Count == 1)
                    {
                        Post p = this.posts[0];
                        if (p.PreviousPost != null) ps.Add(p.PreviousPost.Url);
                        ps.Add(p.Url);
                        if (p.NextPost != null) ps.Add(p.NextPost.Url);
                    }
                    else
                    {
                        for (int i = 1; i <= this.posts.PageCount(this.pager.PageSize); i++)
                        {
                            ps.Add(this.MakePageLink(i));
                        }
                    }
                }
                return ps;
            }
        }

        public Link NextPageLink
        {
            get 
            {
                this.EnsurePosts();
                if (this.posts.Count == 1 && this.posts[0].NextPost != null) return this.posts[0].NextPost.Url;
                else if (this.posts.Count != 1 && this.PostCollection.PageCount(this.pager.PageSize) > this.pager.CurrentPageNumber) return this.MakePageLink(this.pager.CurrentPageNumber + 1);
                return null;
            }
        }

        public Link PreviousPageLink
        {
            get
            {
                this.EnsurePosts();
                if (this.posts.Count == 1 && this.posts[0].PreviousPost != null) return this.posts[0].PreviousPost.Url;
                else if (this.posts.Count != 1 && this.pager.CurrentPageNumber > 1) return this.MakePageLink(this.pager.CurrentPageNumber - 1);
                return null;
            }
        }

        public Link CurrentPageLink
        {
            get
            {
                this.EnsurePosts();
                if (this.posts.Count == 1) return this.posts[0].Url;
                else if (this.posts.Count != 1) return this.MakePageLink(this.pager.CurrentPageNumber);
                return null;
            }
        }

        public bool HasPages
        {
            get { return true; }
        }

        Link MakePageLink(int pageNumber)
        {
            return new Link(this.Url.Path, string.Format(this.PageLinkFormat, pageNumber), string.Format("Page {0}", pageNumber));
        }

        /// <summary>
        /// Gets the link format for the current list
        /// </summary>
        string PageLinkFormat
        {
            get
            {
                string url = "/p{0}";
                if (Params["Year"] != null && Params["Month"] != null && Params["Day"] != null)
                {
                    url = string.Format("/{0}/{1}/{2}/{3}",
                        Params["Year"],
                        Params["Month"],
                        Params["Day"],
                        "p{0}"
                        );
                }
                else if (Params["Year"] != null && Params["Month"] != null)
                {
                    url = string.Format("/{0}/{1}/{2}",
                        Params["Year"],
                        Params["Month"],
                        "p{0}"
                        );
                }
                else if (Params["Year"] != null)
                {
                    url = string.Format("/{0}/{1}",
                        Params["Year"],
                        "p{0}"
                        );
                }
                return url;
            }
        }

        #endregion
    }
}
