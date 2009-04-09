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
using System.Collections.Generic;
using Mubble.Data;

namespace Mubble.Admin
{
    public class RoutingModule : System.Web.Routing.UrlRoutingModule
    {
        public override void PostMapRequestHandler(HttpContextBase context)
        {
            base.PostMapRequestHandler(context);

            var ri = context.Items["mubble_RequestItems"] as RequestItems;

            if (ri != null)
            {
                context.RewritePath(ri.OriginalPath);
            }

            //RequestData data = (RequestData)context.Items[_requestDataKey];
            //if (data != null)
            //{
            //    context.RewritePath(data.OriginalPath);
            //    context.Handler = data.HttpHandler;
            //}
        }

        public override void PostResolveRequestCache(HttpContextBase context)
        {
            /*
             * New routes:
             *  Path.format
             */


            // .html == html
            // .xml == xml

            var formats = new Dictionary<string, string>
            {
                { ".html", "html" },
                { ".xml", "xml" },
                { ".media", "media" }
            };

            var format = (string)null;
            var contentPath = (string)null;
            var parameters = (string)null;
            var ci = (IContentItem)null;

            var ri = (RequestItems)null;

            var path = context.Request.AppRelativeCurrentExecutionFilePath.Substring(1);

            if (!path.StartsWith("/@"))
            {
                while (!char.IsLetterOrDigit(path[path.Length - 1]))
                {
                    path = path.Substring(0, path.Length - 1);
                }
                if (!path.Contains('.')) path = path + ".html";

                if ("/default.aspx".Equals(path, StringComparison.CurrentCultureIgnoreCase))
                {
                    ri = new RequestItems
                    {
                        Content = ContentItem.GetResolved("/"),
                        Format = "html",
                        OriginalPath = context.Request.Path
                    };
                }
                else
                {
                    foreach (var key in formats.Keys)
                    {
                        var startIndex = path.IndexOf(key, StringComparison.CurrentCultureIgnoreCase);
                        if (startIndex > -1)
                        {
                            format = formats[key];
                            contentPath = path.Substring(0, startIndex);
                            ci = ContentItem.GetResolved(contentPath);
                            parameters = path.Substring(startIndex + key.Length);

                            ri = new RequestItems
                            {
                                Content = ci,
                                Format = format,
                                OriginalPath = context.Request.Path
                            };
                            break;
                        }
                    }
                }

                if (ri != null)
                {
                    context.Items["mubble_RequestItems"] = ri;

                    var newPath = parameters;

                    if (ci is Article) newPath = "/article" + newPath;
                    else newPath = "/content" + newPath;

                    if (format == "media")
                    {
                        newPath = "/file" + parameters;
                    }

                    context.RewritePath(newPath);
                }
            }

            base.PostResolveRequestCache(context);

            //RouteData routeData = this.RouteCollection.GetRouteData(context);
            //if (routeData != null)
            //{
            //    Type routeHandler = routeData.Route.RouteHandler;
            //    if (routeHandler == null)
            //    {
            //        throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, AtlasWeb.UrlRoutingModule_NoRouteHandler, new object[] { routeData.Route.Url }));
            //    }
            //    IRouteHandler handler = (IRouteHandler)Activator.CreateInstance(routeHandler);
            //    RequestContext requestContext = new RequestContext(context, routeData);
            //    IHttpHandler httpHandler = handler.GetHttpHandler(requestContext);
            //    if (httpHandler == null)
            //    {
            //        throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, AtlasWeb.UrlRoutingModule_NoHttpHandler, new object[] { routeHandler }));
            //    }
            //    RequestData data2 = new RequestData();
            //    data2.OriginalPath = context.Request.Path;
            //    data2.HttpHandler = httpHandler;
            //    context.Items[_requestDataKey] = data2;
            //    context.RewritePath("~/Mvc.axd");
            //}
        }
    }
}
