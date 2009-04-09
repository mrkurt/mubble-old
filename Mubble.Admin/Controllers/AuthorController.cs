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
using System.Web.Mvc;
using Mubble.Data;

namespace Mubble.Admin.Controllers
{
    public class AuthorController : Controller
    {
        public void List()
        {
            var authors = Author.List();
            var ci = new ListViewData<Author>(authors);
            ci.CurrentContent = ContentItem.Get("/");

            Response.ContentType = "text/xml";
            Response.Write(Mubble.Data.Serialization.Xml.Serialize(authors));

            //RenderView("List", ci);
        }

        public void Get(string username)
        {
            var author = Author.Get(username);

            //var s = Mubble.Data.Serialization.Xml.Serialize(author);

            //Response.ContentType = "text/xml";
            //Response.Write(s);

            var v = new SingleItemViewResponse<Author>(author);
            v.CurrentContent = ContentItem.Get("/");

            //RenderView("Get", v);
        }
    }
}
