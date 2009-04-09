using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using ActiveObjects;

namespace Mubble.UI.Data
{
    //TODO: Build various IDataSource objects
    public class List : Mubble.UI.Repeater, IPaging
    {
        #region Properties
        private List<IDataSource> data;

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<IDataSource> Data
        {
            get { return data; }
            set { data = value; }
        }

        private Pager pager;

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public Pager Pager
        {
            get { return pager; }
            set { pager = value; }
        }
        public long TotalResults
        {
            get { return this.dataList != null ? this.dataList.TotalResults : 0; }
        }

        private int offset = 0;

        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        private int maxResults = 20;

        public int MaxResults
        {
            get { return maxResults; }
            set { maxResults = value; }
        }

        public int Count
        {
            get 
            {
                this.EnsureData();
                IEnumerable<IActiveObject> data = this.DataSource as IEnumerable<IActiveObject>;
                int count = 0;
                if (data != null)
                {
                    foreach(IActiveObject obj in data)
                    {
                        count++;
                    }
                }
                else
                {
                    count = this.dataList != null ? this.dataList.Count : 0;
                }

                return count; 
            }
        }
        #endregion

        public List()
            : base()
        {
            this.Load += new EventHandler(
                    delegate(object sender, EventArgs e)
                    {
                        this.EnsureData();
                        this.DataBind();
                    }
                );
        }

        protected override System.Web.UI.WebControls.RepeaterItem CreateItem(int itemIndex, System.Web.UI.WebControls.ListItemType itemType)
        {
            return new ListRepeaterItem(itemIndex, itemType);
        }

        bool bound = false;

        public override void DataBind()
        {
            if (!bound)
            {
                bound = true;
                base.DataBind();
            }
        }

        private IActiveCollection dataList;

        public void EnsureData()
        {
            if (this.Pager != null) this.Pager.SetParent(this);
            if (this.dataList == null)
            {
                int startIndex = this.Offset;
                int endIndex = startIndex + this.MaxResults;

                if (this.Pager != null)
                {
                    if (this.Pager.PageSize > 0)
                    {
                        startIndex = (this.Pager.PageNumber - 1) * this.Pager.PageSize;
                        endIndex = startIndex + this.Pager.PageSize;
                    }
                }

                this.dataList = new ActiveCollection<IActiveObject>();
                IDataSource dataSource = null;
                foreach (IDataSource ds in this.Data)
                {
                    dataSource = ds;
                    ds.Parent = this;
                    IActiveCollection col = ds.GetData(startIndex, endIndex);
                    this.dataList = col != null ? col : this.dataList;
                    break;
                }
                this.DataSource = this.dataList.Count > 0 && dataSource != null ? dataSource.FilterData(this.dataList) : null;
            }
        }

        #region IPaging Members

        private Mubble.Models.RouteParameters GetRouteParameters()
        {
            Mubble.Models.RouteParameters p = new Mubble.Models.RouteParameters();
            foreach (string key in this.Params.Keys)
            {
                p.Add(key, this.Params[key]);
            }
            return p;
        }

        void EnsurePager()
        {
            if (this.Pager == null)
            {
                throw new NullReferenceException("Paging is not available for List controls without a \"Pager\" parameter");
            }
        }

        public List<Mubble.Models.Link> PageLinks
        {
            get 
            {
                this.EnsureData();
                this.EnsurePager();
                List<Mubble.Models.Link> pages = new List<Mubble.Models.Link>();
                Mubble.Models.RouteParameters p = this.GetRouteParameters();
                for (int i = 1; i <= this.Pager.TotalPages; i++)
                {
                    p["Page"] = i;
                    Mubble.Models.Link l = this.Content.BuildLink(p);
                    l.Title = i.ToString();
                    pages.Add(l);
                }
                return pages;
            }
        }

        private Mubble.Models.Link GetPageFromCurrent(int offset)
        {
            this.EnsureData();
            this.EnsurePager();
            int newPage = this.Pager.PageNumber + offset;
            if (newPage < 1 || newPage > this.Pager.TotalPages) return null;
            Mubble.Models.RouteParameters p = this.GetRouteParameters();
            p["Page"] = newPage;

            Mubble.Models.Link l = this.Content.BuildLink(p);
            l.Title = (newPage).ToString();
            return l;
        }

        public Mubble.Models.Link NextPageLink
        {
            get { return this.GetPageFromCurrent(1);  }
        }

        public Mubble.Models.Link PreviousPageLink
        {
            get { return this.GetPageFromCurrent(-1); }
        }

        public Mubble.Models.Link CurrentPageLink
        {
            get { return this.GetPageFromCurrent(0); }
        }

        public bool HasPages
        {
            get { return this.Pager != null; }
        }

        #endregion
    }

    public class ListRepeaterItem : Mubble.UI.RepeaterTemplate, IScoped
    {
        public ListRepeaterItem(int itemIndex, System.Web.UI.WebControls.ListItemType itemType) : base(itemIndex, itemType) { }

        private IActiveObject activeObject;

        public IActiveObject ActiveObject
        {
            get { return activeObject; }
            set { activeObject = value; }
        }


        public override object DataItem
        {
            get { return base.DataItem; }
            set 
            { 
                base.DataItem = value;
                if (value != null)
                {

                    this.ActiveObject = value as IActiveObject;
                }
            }
        }

        #region IScoped Members

        public object Scope
        {
            get { return this.ActiveObject; }
        }

        #endregion
    }
}
