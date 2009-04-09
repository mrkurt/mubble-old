using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Handlers
{
    public class Javascript : System.Web.IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public static string Version
        {
            get { return "1"; }
        }

        public void ProcessRequest(System.Web.HttpContext context)
        {
            string[] handlers = new string[] { "HtmlHandler", "JsonHandler" };

            if (context.User.Identity.IsAuthenticated) handlers = new string[] { "HtmlHandler", "JsonHandler", "AdminHandler" };

            context.Response.Write("var Mubble = {" + Environment.NewLine);
            foreach(string handler in handlers)
            {
                context.Response.Write(
                    string.Format("\t{0} : '{1}',\r\n", handler, Settings.Extensions.FindByKey(handler))
                    );
            }
            context.Response.Write(string.Format("\tGeneratedAt : '{0}'\r\n}};\r\n", DateTime.Now));
        }

        #endregion
    }
}
