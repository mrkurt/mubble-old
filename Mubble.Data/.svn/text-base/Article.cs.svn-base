using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.Data
{
    public partial class Article : Published<Article>
    {
        private List<Page> pages;
        public List<Page> Pages
        {
            get
            {
                if (pages == null)
                {
                    pages = Page.List(p => p.ContentItemID == this.Record.ID);
                }
                return pages;
            }
        }
    }
}