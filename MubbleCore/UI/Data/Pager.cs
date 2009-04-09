using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Mubble.UI.Data
{
    public class Pager
    {
        List Parent;

        private int pageSize = 20;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private int? pageNumber = null;

        public int PageNumber
        {
            get
            {
                if (pageNumber == null)
                {
                    HttpContext context = HttpContext.Current;
                    pageNumber = 1;
                    if (this.Parent.Params.ContainsKey("Page"))
                    {
                        int tmp = 0;
                        int.TryParse(this.Parent.Params["Page"], out tmp);
                        pageNumber = (tmp > 0) ? tmp : pageNumber;
                    }
                    else if (!string.IsNullOrEmpty(context.Request.QueryString["Page"]))
                    {
                        int tmp = 0;
                        int.TryParse(context.Request.QueryString["Page"], out tmp);
                        pageNumber = (tmp > 0) ? tmp : pageNumber;
                    }
                }
                return pageNumber.Value;
            }
            set { pageNumber = value; }
        }

        public int TotalPages
        {
            get { return (this.Parent.TotalResults > 0 && this.PageSize > 0) ? (int)Math.Ceiling(((double)this.Parent.TotalResults / (double)this.PageSize)) : 0; }
        }

        public void SetParent(List parent)
        {
            this.Parent = parent;
        }
    }
}
