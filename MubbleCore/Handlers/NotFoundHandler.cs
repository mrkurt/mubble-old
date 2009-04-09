using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Mubble.Handlers
{
    public class NotFoundHandler : IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            Uri requestedUri = context.Request.Url;
            string requested = null;
            if (context.Request.QueryString["aspxerrorpath"] != null && context.Request.QueryString["aspxerrorpath"].Length > 0)
            {
                requested = context.Request.QueryString["aspxerrorpath"];
            }
            else if (context.Request.RawUrl.IndexOf("?404;") >= 0)
            {
                requested = context.Request.RawUrl.Substring(context.Request.RawUrl.IndexOf("?404;"));
                requested = requested.Substring(5);

                Uri req = new Uri(requested);
                requested = req.PathAndQuery;
            }

            if (string.IsNullOrEmpty(requested))
            {
                Write404(context);
                return;
            }
            

            if (requested != null && context.Request.ApplicationPath.Length > 1 && requested.IndexOf(context.Request.ApplicationPath) == 0)
            {
                requested = requested.Substring(context.Request.ApplicationPath.Length);
            }

            string path, handler, extra;

            if (!Mubble.Models.UrlRedirect.FindRedirectUrl(requested, out path, out handler, out extra))
            {
                string u = Mubble.Config.Redirects.Current.FindRedirect(requestedUri.ToString());
                if (u != null)
                {
                    Mubble.Tools.Http.RedirectPermanently(u);
                    return;
                }
            }
            else
            {
                Mubble.Tools.Http.RedirectPermanently(MubbleUrl.Create(path, extra, handler));
                //context.Response.Write(MubbleUrl.Create(path, extra, handler).ToString());
                return;
            }

            Write404(context);
        }

        #endregion

        void Write404(HttpContext context)
        {
            context.Response.Write("The requested file was not found");
            context.Response.StatusCode = 404;
        }
    }
}
