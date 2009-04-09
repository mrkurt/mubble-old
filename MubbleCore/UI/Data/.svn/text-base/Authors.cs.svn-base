using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Mubble.Models;

namespace Mubble.UI.Data
{
    public class Authors : Mubble.UI.BlankRepeater
    {

        private List<IAuthorFilter> filters = new List<IAuthorFilter>();

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<IAuthorFilter> Filters
        {
            get { return filters; }
            set { filters = value; }
        }


        [TemplateContainer(typeof(AuthorRepeaterTemplate)), PersistenceMode(PersistenceMode.InnerProperty), Browsable(false)]
        public System.Web.UI.ITemplate AuthorTemplate
        {
            get { return this.ItemTemplate; }
            set { this.ItemTemplate = value; }
        }

        /// <summary>
        /// Gets the current working set of filters
        /// </summary>
        protected List<IAuthorFilter> GetFilters()
        {
            int startIndex = 0;
            List<IAuthorFilter> set = new List<IAuthorFilter>(this.Filters);
            if (set.Count == 0)
            {
                set.Insert(0, new ScopeFilter());
            }
            for (int i = 0; i < set.Count; i++)
            {
                set[i].Parent = this;
                if (set[i] is ClearFilters) startIndex = i;
            }
            return set.GetRange(startIndex, set.Count - startIndex);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            Dictionary<string, object> parameters = null;
            foreach (IAuthorFilter filter in this.GetFilters())
            {
                if (filter is ScopeFilter)
                {
                    IAuthors authors = Control.GetCurrentScope<IAuthors>(this);
                    if(authors != null)
                    {
                        IList<string> a = authors.GetAuthors();
                        List<Author> items = new List<Author>(a.Count);
                        foreach (string name in a)
                        {
                            items.Add(DataBroker.GetAuthor(name));
                        }
                        this.DataSource = items;
                    }
                }
                else
                {
                    if(parameters == null) parameters = new Dictionary<string, object>();
                    filter.Before(parameters);
                }
            }

            if (parameters != null)
            {
                this.DataSource = Author.Find(parameters);
            }
            if(this.DataSource != null)
            {
                base.DataBind();
                if (this.Items.Count > 0)
                {
                    base.Render(writer);
                }
            }
        }

        protected override System.Web.UI.WebControls.RepeaterItem CreateItem(int itemIndex, System.Web.UI.WebControls.ListItemType itemType)
        {
            AuthorRepeaterTemplate item = new AuthorRepeaterTemplate(itemIndex, itemType);
            item.Controller = this.Content;
            return item;
        }

        /// <summary>
        /// Gets or sets the location of the author template file
        /// </summary>
        public string AuthorTemplatePath
        {
            get { return this.ItemTemplatePath; }
            set { this.ItemTemplatePath = value; }
        }
    }
}
