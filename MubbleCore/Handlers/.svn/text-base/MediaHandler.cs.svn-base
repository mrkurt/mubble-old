using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Web.Caching;

namespace Mubble.Handlers
{
    /// <summary>
    /// Handles requests for content Media
    /// </summary>
    public class MediaHandler : HttpHandler
    {
        private static readonly log4net.ILog log = 
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void ProcessMubbleRequest(System.Web.HttpContext context)
        {
            try
            {

                string parameterPattern = @"(?<type>\w){(?<value>\w+?)}";

                string hay = Url.PathExtra;
                string fileName = Url.PathItems[Url.PathItems.Length - 1];

                Mubble.Models.File media = DataBroker.GetFile(Controller.ID, fileName);

                if (media == null || !media.FileExists)
                {
                    context.Response.Write("The requested file was not found");
                    context.Response.StatusCode = 404;
                    return;
                }

                int width = 0, height = 0;

                Regex r = new Regex(parameterPattern, RegexOptions.IgnoreCase);

                string[] parts = Url.PathExtra.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                string cleanedHay = "";

                foreach (string p in parts)
                {
                    if (cleanedHay.Length > 0) cleanedHay += "/";
                    cleanedHay += context.Server.UrlDecode(p);
                }

                MatchCollection matches = r.Matches(cleanedHay);

                bool resize = false;

                foreach (Match m in matches)
                {
                    switch (m.Groups["type"].Value.ToLower())
                    {
                        case "w":
                            //sets width
                            int.TryParse(m.Groups["value"].Value, out width);
                            if (width > 0)
                            {
                                resize = true;
                            }
                            break;
                        case "h":
                            //sets height
                            int.TryParse(m.Groups["value"].Value, out height);
                            if (height > 0)
                            {
                                resize = true;
                            }
                            break;
                    }
                }

                if (matches.Count == 0)
                {
                    if (width == 0 && parts.Length > 1)
                    {
                        int.TryParse(parts[0], out width);
                        resize = true;
                    }
                    if (height > 0 && parts.Length > 2)
                    {
                        int.TryParse(parts[2], out height);
                        resize = true;
                    }
                }

                string file = media.GetThumbnail(width, height);

                //FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);

                //byte[] contents = new byte[stream.Length];

                //stream.Read(contents, 0, contents.Length);

                //stream.Close();

                //context.Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);

                var fileInfo = new FileInfo(file);

				context.Response.Cache.SetCacheability(HttpCacheability.Public);
				context.Response.Cache.SetExpires(DateTime.Now.AddYears(1));
                context.Response.Cache.SetLastModified(fileInfo.LastWriteTime);
				context.Response.Cache.SetOmitVaryStar(true);

                context.Response.ContentType = (resize) ? media.ThumbnailMimeType : media.MimeType;

                context.Response.TransmitFile(file);

                //context.Response.OutputStream.Write(contents, 0, contents.Length);
            }
            catch (Exception ex)
            {
                if (!(ex is HttpException))
                {
                    log.Error("Serving media failed", ex);
                }
                context.Response.StatusCode = 404;
                context.Response.Write("Media does not exist");
            }
            
        }
}
}
