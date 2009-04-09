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
using System.Web.Mvc;

namespace Mubble.Admin.Controllers
{
    public class ContentItemController : Controller
    {
        private RequestItems ri;
        public RequestItems RequestItems
        {
            get
            {
                if (ri == null)
                {
                    ri = System.Web.HttpContext.Current.Items["mubble_RequestItems"] as RequestItems;
                }
                return ri;
            }
        }

        public IContentItem CurrentContentItem
        {
            get { return this.RequestItems.Content; }
        }

        public string Format
        {
            get { return this.RequestItems.Format; }
        }

        public void Get(string path)
        {
            var ci = this.CurrentContentItem;
            var response = new SingleItemViewResponse<IContentItem>(ci);

            //Response.ContentType = "text/xml";
            Response.Write(Mubble.Web.Output.Utility.Render<Mubble.Web.Output.Json>(ci));
        }

        public void List()
        {
            var section = this.CurrentContentItem as Section;
            var items = section.List(1, 40);

            var vd = new ListViewData<IContentItem>(items, section);

            //RenderView("List", vd);
            
        }
    }
}
