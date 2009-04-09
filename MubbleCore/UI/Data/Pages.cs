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
    public class Pages : Mubble.UI.BlankRepeater, IPaging
    {
        protected ActiveCollection<Mubble.Models.Page> pages;
        protected Mubble.Models.Page currentPage;
        protected int currentPageIndex = -1;

        private List<IFilter> filters = new List<IFilter>();

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<IFilter> Filters
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


        #region Filtering
        /// <summary>
        /// Gets the current working set of filters
        /// </summary>
        protected List<IFilter> GetFilters()
        {
            int startIndex = 0;
            List<IFilter> set = new List<IFilter>(this.Filters);
            set.Insert(0, new ScopeFilter());
            for (int i = 0; i < set.Count; i++)
            {
                set[i].Parent = this;
                if (set[i] is ClearFilters) startIndex = i;
            }
            return set.GetRange(startIndex, set.Count - startIndex);
        }
        protected void GetPages()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            PageNumber pager = null;
            bool useScopeOnly = true;
            foreach(IFilter filter in this.GetFilters())
            {
                if (filter is PageNumber)
                {
                    pager = (PageNumber)filter;
                }
                else
                {
                    if (!(filter is ScopeFilter))
                    {
                        useScopeOnly = false;
                    }
                    this.ApplyFilter(filter, parameters);
                }
            }

            this.pages = (useScopeOnly) ? Content.Pages : Mubble.Models.Page.Find(parameters);

            if (pager != null)
            {
                for (int i = 0; i < this.pages.Count; i++)
                {
                    if (this.pages[i].PageNumber == pager.CurrentPageNumber)
                    {
                        this.DataSource = new Mubble.Models.Page[] { currentPage = this.pages[this.currentPageIndex = i] };
                        this.Page.SetPageTitle(currentPage.Controller.Title + ": Page " + currentPage.PageNumber);
                        break;
                    }
                }
            }
            else
            {
                this.DataSource = this.pages;
            }
            base.DataBind();
        }
        protected void ApplyFilter(IFilter filter, Dictionary<string, object> parameters)
        {
            ScopeFilter sf = filter as ScopeFilter;
            if (sf != null)
            {
                Article article = Control.GetCurrentScope<Article>(this);
                if (article != null && !parameters.ContainsKey("ControllerID"))
                {
                    parameters.Add("ControllerID", article.ID);
                }
            }
        }
        #endregion

        #region Templates
        [TemplateContainer(typeof(AuthorRepeaterTemplate)), PersistenceMode(PersistenceMode.InnerProperty), Browsable(false)]
        public System.Web.UI.ITemplate PageTemplate
        {
            get { return this.ItemTemplate; }
            set { this.ItemTemplate = value; }
        }

        protected override System.Web.UI.WebControls.RepeaterItem CreateItem(int itemIndex, System.Web.UI.WebControls.ListItemType itemType)
        {
            PageRepeaterTemplate item = new PageRepeaterTemplate(itemIndex, itemType);
            item.Controller = this.Content;
            return item;
        }

        /// <summary>
        /// Gets or sets the location of the page template file
        /// </summary>
        public string PageTemplatePath
        {
            get { return this.ItemTemplatePath; }
            set { this.ItemTemplatePath = value; }
        }
        #endregion

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            this.EnsurePages();
            if (this.Items.Count > 0)
            {
                base.Render(writer);
            }
        }

        public Pages()
        {
            this.Load += new EventHandler(Pages_Load);
        }

        void Pages_Load(object sender, EventArgs e)
        {
            this.EnsurePages();
        }


        #region IPaging Members and helpers

        protected void EnsurePages()
        {
            if (pages == null)
            {
                this.GetPages();
            }
        }

        public List<Link> PageLinks
        {
            get 
            { 
                this.EnsurePages(); 
                List<Link> ps = new List<Link>();
                if (this.pages != null)
                {
                    foreach (Mubble.Models.Page p in this.pages) ps.Add(p.Url);
                }
                return ps;
            }
        }

        public Link NextPageLink
        {
            get { this.EnsurePages(); return (this.currentPageIndex < this.pages.Count - 1) ? this.pages[this.currentPageIndex + 1].Url : null; }
        }

        public Link PreviousPageLink
        {
            get { this.EnsurePages(); return (this.currentPageIndex >= 1) ? this.pages[this.currentPageIndex - 1].Url : null;  }
        }

        public Link CurrentPageLink
        {
            get { this.EnsurePages(); return (this.currentPage != null) ? this.currentPage.Url : null; }
        }

        public bool HasPages
        {
            get { this.EnsurePages(); return this.pages.Count > 1; }
        }

        #endregion
    }
}
