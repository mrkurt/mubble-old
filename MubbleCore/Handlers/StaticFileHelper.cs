using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace Mubble.Handlers
{
    public class StaticFileHelper : IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        static Dictionary<string, string> extensions = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);

        static StaticFileHelper()
        {
            extensions.Add(".css", "text/css");
            extensions.Add(".jpg", "image/jpeg");
            extensions.Add(".png", "image/png");
            extensions.Add(".gif", "image/gif");
            extensions.Add(".js", "application/x-javascript");
            extensions.Add(".htc", "text/x-component");
        }

        public void ProcessRequest(HttpContext context)
        {
            string templateDir = context.Request.FilePath.Substring(context.Request.FilePath.LastIndexOf('/'));
            templateDir = templateDir.Substring(0, templateDir.IndexOf('.'));
            templateDir = string.Concat("~/Templates", templateDir);

            string path = context.Request.PathInfo;

            path = Regex.Replace(path, @"\.v\d+\.", ".");
            path = templateDir + path;
            string physicalPath = context.Server.MapPath(path);
            System.IO.FileInfo info = new System.IO.FileInfo(physicalPath);

            string mime = null;

            extensions.TryGetValue(info.Extension, out mime);

            if (mime != null && info.Exists)
            {
				context.Response.Cache.SetCacheability(HttpCacheability.Public);
                context.Response.Cache.SetExpires(DateTime.Now.AddDays(1));
                context.Response.Cache.SetLastModified(info.LastWriteTime);
				context.Response.Cache.SetOmitVaryStar(true);
                context.Response.ContentType = mime;
                context.Response.TransmitFile(physicalPath);
            }
            else
            {
                context.Response.StatusCode = 404;
                context.Response.Write("File not found.");
            }
        }

        #endregion
    }
}
