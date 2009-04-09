using System;
using System.Collections.Generic;
using System.Text;
using Mubble.UI.WebControls;

namespace Mubble.UI
{
    public abstract class FeaturedContentControl : Mubble.UI.CompositeControl, Mubble.UI.WebControls.IPageable
    {
        private int pageNumber;

        /// <summary>
        /// Gets or sets the page number to use
        /// </summary>
        public int PageNumber
        {
            get 
            {
                return pageNumber; 
            }
            set 
            {
                if (value <= this.Content.Pages.Count && value >= 1)
                {
                    pageNumber = value;
                }
                else if (value >= 1)
                {
                    pageNumber = this.Content.Pages.Count;
                }
                else
                {
                    pageNumber = 1;
                }
            }
        }

        private Mubble.Models.Page selectedPage;

        /// <summary>
        /// Gets the particular FeaturedContentPage specified by the control
        /// </summary>
        public Mubble.Models.Page SelectedPage
        {
            get
            {
                if (selectedPage == null && this.PageNumber > 0)
                {
                    selectedPage = this.Content.GetPage(this.PageNumber);
                }
                return selectedPage;
            }
        }


        /*private MubbleList<Mubble.FeaturedContentPage> allPages;

        /// <summary>
        /// Gets the list of pages associated with the selected content
        /// </summary>
        public MubbleList<Mubble.FeaturedContentPage> AllPages
        {
            get
            {
                if (allPages == null)
                {
                    allPages = FeaturedContent.Pages;
                }
                return allPages;
            }
        }*/


        #region IPageable Members

        public PagePair CurrentPage
        {
            get { return this.GetPagePair(this.PageNumber); }
        }

        public PagePair NextPage
        {
            get 
            { 
                int newPage = this.PageNumber + 1;
                return this.GetPagePair(newPage);
            }
        }

        public PagePair PreviousPage
        {
            get 
            {
                int newPage = this.PageNumber - 1;
                return this.GetPagePair(newPage);
            }
        }

        public List<PagePair> AllPages
        {
            get 
            {
                List<Mubble.UI.WebControls.PagePair> list = new List<Mubble.UI.WebControls.PagePair>();
                foreach (Mubble.Models.Page p in this.Content.Pages)
                {
                    list.Add(this.GetPagePair(p));
                }
                return list;
            }
        }

        protected string PageLinkFormat
        {
            get { return Page.Url.ToString(Page.Url.Handler, "/{0}"); }
        }

        protected PagePair GetPagePair(int pageNumber)
        {
            Mubble.Models.Page p = this.Content.GetPage(pageNumber);
            if (p == null) return null;
            else return this.GetPagePair(p);
        }

        protected PagePair GetPagePair(Mubble.Models.Page contentPage)
        {

            PagePair pPair = new PagePair();
            pPair.Link = string.Format(this.PageLinkFormat, contentPage.PageNumber);
            pPair.Name = (contentPage.Name == null || contentPage.Name.Trim() == string.Empty) 
                ? "Page " + contentPage.PageNumber : contentPage.Name; ;

            return pPair;
        }

        public bool IsPageable { get { return true; } }

        #endregion
    }
}
