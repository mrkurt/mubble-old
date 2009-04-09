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

namespace Mubble.Admin
{
    public class RequestItems
    {
        public IContentItem Content { get; set; }
        public string Format { get; set; }
        public string OriginalPath { get; set; }
    }
}
