using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Mubble.Data;
using System.Collections.Generic;

namespace Mubble.Admin.Controllers
{
    public class ListViewData<T> : BaseViewData
        where T : IData
    {
        public PagedList<T> List { get; set; }

        public ListViewData(IList<T> list) : this(list, null) { }
        public ListViewData(IList<T> list, IContentItem currentContent)
        {
            this.CurrentContent = currentContent;

            this.List = new PagedList<T>(list, list.Count, 1, list.Count);
        }

        public ListViewData(PagedList<T> list, IContentItem currentContent)
        {
            this.List = list;
            this.CurrentContent = currentContent;
        }
    }
}
