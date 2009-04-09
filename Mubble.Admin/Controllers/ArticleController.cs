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

namespace Mubble.Admin.Controllers
{
    public class ArticleController : ContentItemController
    {
        public void Page(int page)
        {
            Response.Write("Article page " + page);
        }
    }
}
