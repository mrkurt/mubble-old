using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Configuration;
using System.Configuration;
using System.Security.Permissions;
using System.Web.Security;

using Mubble.Models;
using System.Data.SqlClient;

namespace Mubble.UI
{
    public class Global : System.Web.HttpApplication
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        void Application_Start(Object sender, EventArgs e)
        {
            Benchmark b = new Benchmark(true);
            System.IO.FileInfo logFile = new System.IO.FileInfo(Server.MapPath("Mubble.log4net"));
            SqlServerMetrics.Init();
            log4net.Config.XmlConfigurator.ConfigureAndWatch(logFile);
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~/");
            HttpHandlersSection handlers = (HttpHandlersSection)configuration.GetSection("system.web/httpHandlers");

            foreach (HttpHandlerAction handler in handlers.Handlers)
            {
                Type t = Type.GetType(handler.Type);
                if (t != null &&
                    t.IsSubclassOf(typeof(Handlers.HttpHandler)) &&
                    handler.Path.IndexOf('*') == 0 && 
                    !Mubble.Handlers.Settings.Extensions.ContainsKey(t.FullName))
                {
                   Mubble.Handlers.Settings.Extensions.Add(t.FullName, handler.Path.Substring(1));
               }
               else if (t != null && t == typeof(Mubble.Handlers.StaticFileHelper) && handler.Path.StartsWith("*"))
               {
                   Config.Caching.StaticFileHelperExtension = handler.Path.Substring(1);
               }
            }

            // Set various middle tier object configuration settings
            ConnectionStringsSection section = (ConnectionStringsSection)
                    WebConfigurationManager.GetSection("connectionStrings");
            ActiveObjects.SqlServer.SqlDataUtility.ReadDB = section.ConnectionStrings["mubbleDBRead"].ConnectionString;
            ActiveObjects.SqlServer.SqlDataUtility.WriteDB = section.ConnectionStrings["mubbleDBRead"].ConnectionString;

            Controller.RootContentPath = WebConfigurationManager.AppSettings["DefaultContent"];
            Controller.RootContent = DataBroker.GetController(Controller.RootContentPath);

            File.FileStoreBase = Server.MapPath(WebConfigurationManager.AppSettings["StoreBase"]);

            MubbleUrl.ApplicationPath = this.Context.Request.ApplicationPath;

            // Loads new modules
            Mubble.Config.Module.LoadDbFromFileSystem(Server.MapPath("~/Modules"), "~/Modules/");

            Mubble.Models.Settings.Lucene lucene = new Mubble.Models.Settings.Lucene();
            lucene.IndexLocation = Server.MapPath(WebConfigurationManager.AppSettings["LuceneIndexLocation"]);
            Mubble.Models.Settings.Application.Lucene = lucene;

            Mubble.Models.Settings.Web web = new Mubble.Models.Settings.Web();
            web.ApplicationPath = this.Context.Request.ApplicationPath;;
            Mubble.Models.Settings.Application.Web = web;

            Mubble.Models.QueryEngine.Engine.CurrentEngine =
                            Mubble.Models.QueryEngine.Engines.Lucene.Init(lucene.IndexLocation, Config.Index.Current.UseRamSearcher);
            //Mubble.Models.QueryEngine.Engines.Lucene.Init(lucene.IndexLocation, Config.Index.Current.UseRamSearcher);

            //Mubble.Caching.CacheLocation = System.IO.Path.Combine(File.FileStoreBase, "CacheKeys");

            //DataBroker.Warmup();

            CacheBroker.Init();

            Mubble.Models.ScheduledCommand.StartTimer();

            b.End();

            log.InfoFormat("Application startup too {0}", b.ElapsedTime);
        }

        void Application_End(Object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        void Application_Error(Object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(Object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(Object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

        public override string GetVaryByCustomString(System.Web.HttpContext context, string custom)
        {
            return Caching.VaryByCustom(context, custom);
        }
    }
}
