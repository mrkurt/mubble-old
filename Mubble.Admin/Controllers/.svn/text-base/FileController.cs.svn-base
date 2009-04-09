using System;
using System.Web;
using System.Web.Mvc;
using Mubble.Data;

namespace Mubble.Admin.Controllers
{
    public class FileController : Controller
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

        public void Get(string filename, string extension)
        {
            filename = filename + "." + extension;
            //leadpic_newman2.jpg
            var f = File.Get(this.CurrentContentItem.ID, filename);
            Response.Write("This would have served up " + filename);
            Response.Write("<br />");
            Response.Write("From " + f.PhysicalPath);
        }
    }
}
