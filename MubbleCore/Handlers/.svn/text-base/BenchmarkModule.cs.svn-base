using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Mubble.Handlers
{
    public class BenchmarkModule : System.Web.IHttpModule
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region IHttpModule Members

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            if (Config.Benchmarks.Current.Enabled)
            {
                context.BeginRequest += new EventHandler(BenchmarkModule.BeginRequest);
                context.EndRequest += new EventHandler(BenchmarkModule.EndRequest);
            }
            context.Error += new EventHandler(BenchmarkModule.RequestError);
        }

        static void RequestError(object sender, EventArgs e)
        {
            try
            {
                HttpApplication app = sender as HttpApplication;
                if (app == null || app.Server == null) return;

                Exception ex = app.Server.GetLastError();

                HttpException httpError = ex as HttpException;

                if (httpError == null || httpError.GetHttpCode() != 404)
                {
                    log.Error(string.Format("An error occured while processing {0} for {1}", app.Context.Request.RawUrl, app.Context.Request.UserHostAddress), ex);
                }
            }
            catch { }
        }

        static void BeginRequest(object sender, EventArgs e)
        {
            try
            {
                HttpApplication app = sender as HttpApplication;
                if (app == null || app.Context == null || app.Context.Items == null) return;

                DateTime start = DateTime.Now;
                app.Context.Items.Add("BenchmarkModule_Start", start);
            }
            catch { }
        }

        static void EndRequest(object sender, EventArgs e)
        {
            try
            {
                HttpApplication app = sender as HttpApplication;
                
                if (app == null || app.Context == null || app.Context.Items == null || !app.Context.Items.Contains("BenchmarkModule_Start")) return;

                if (app.Context.Items["BenchmarkModule_Start"] is DateTime)
                {
                    DateTime start = (DateTime)app.Context.Items["BenchmarkModule_Start"];
                    DateTime end = DateTime.Now;

                    TimeSpan total = end - start;

                    int queries = 0;
                    if (app.Context.Items.Contains("SqlServerQueryCount") && app.Context.Items["SqlServerQueryCount"] is int)
                    {
                        queries = (int)app.Context.Items["SqlServerQueryCount"];
                    }

                    if (total >= Config.Benchmarks.Current.MinTime || queries >= Config.Benchmarks.Current.QueriesToTriggerLog)
                    {
                        log.InfoFormat("Request for {0} took {1:0.####} seconds to load. {2} queries.", 
                            app.Context.Request.RawUrl, 
                            total.TotalSeconds,
                            queries
                            );
                    }
                }

            }
            catch { }
        }

        #endregion

    }
}
