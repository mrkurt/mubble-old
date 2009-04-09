using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using System.Web.Routing;

namespace Mubble.Admin
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add(new Route(
                "article/{page}",
                new RouteValueDictionary
                {
                    { "controller", "Article" },
                    { "action", "Page" },
                    { "page", 1 }
                },
                new MvcRouteHandler()
            ));

            RouteTable.Routes.Add(new Route(
                "content",
                new RouteValueDictionary
                {
                    { "controller", "ContentItem"},
                    { "action", "Get" }
                },
                new MvcRouteHandler()
            ));

            RouteTable.Routes.Add(new Route(
                "files/{filename}.{extension}",
                new RouteValueDictionary
                {
                    { "controller", "File" },
                    { "action", "Get" }
                },
                new MvcRouteHandler()
            ));

            RouteTable.Routes.Add(new Route(
                "Default.aspx",
                new RouteValueDictionary
                {
                    { "controller", "ContentItem" },
                    { "action", "Get" }
                },
                new MvcRouteHandler()
            ));
            
            //RouteTable.Routes.Add(new Route
            //{
            //    Url = "files/[filename].[extension]",
            //    Defaults = new
            //    {
            //        controller = "File",
            //        action = "Get"
            //    },
            //    RouteHandler = typeof(MvcRouteHandler)
            //});

            //RouteTable.Routes.Add(new Route
            //{
            //    Url = "@authors/[username]",
            //    Defaults = new
            //    {
            //        controller = "Author",
            //        action = "Get"
            //    },
            //    RouteHandler = typeof(MvcRouteHandler)
            //});

            //RouteTable.Routes.Add(new Route
            //{
            //    Url = "@authors",
            //    Defaults = new
            //    {
            //        controller = "Author",
            //        action = "List"
            //    },
            //    RouteHandler = typeof(MvcRouteHandler)
            //});

            //RouteTable.Routes.Add(new Route
            //{
            //    Url = "Default.aspx",
            //    Defaults = new { controller = "ContentItem", action = "Get" },
            //    RouteHandler = typeof(MvcRouteHandler)
            //});

            //ControllerBuilder.Current.SetDefaultControllerFactory(typeof(MvcContrib.ControllerFactories.NHamlControllerFactory));
        }
    }
}