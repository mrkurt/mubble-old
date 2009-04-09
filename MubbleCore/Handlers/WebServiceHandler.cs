using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using System.Web.UI;
using System.Web.Services.Protocols;
using System.Web;
using Mubble.Models;

namespace Mubble.Handlers
{
    public class WebServiceHandler : HttpHandler
    {
        public override void ProcessMubbleRequest(System.Web.HttpContext context)
        {
            DateTime startDate = DateTime.Now;

            WebServiceHandlerFactory factory = new WebServiceHandlerFactory();
            IHttpHandler handler = factory.GetHandler(context, "get", "~/WebServices/Content.asmx", "");

            handler.ProcessRequest(context);
        }
}
}
