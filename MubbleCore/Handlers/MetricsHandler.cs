using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Compilation;

namespace Mubble.Handlers
{
    public class MetricsHandler : IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string path = context.Request.Path;
            path = path.Replace(".metrics.", ".").Replace(".ashx", ".aspx");
            if (path.Contains("/"))
            {
                path = path.Substring(path.LastIndexOf('/') + 1);
            }

            path = "~/Admin/Metrics/" + path;

            //path = path);

            if (System.IO.File.Exists(context.Server.MapPath(path)))
            {
                IHttpHandler page = (IHttpHandler)BuildManager.CreateInstanceFromVirtualPath(path, typeof(IHttpHandler));
                page.ProcessRequest(context);
            }
            else
            {
                context.Response.Write("No metrics found");
            }
        }

        #endregion
    }
}
