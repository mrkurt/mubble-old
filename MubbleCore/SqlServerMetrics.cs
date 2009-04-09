using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Mubble
{
    public static class SqlServerMetrics
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Init()
        {
            ActiveObjects.SqlServer.SqlDataUtility.SqlCommandExecuted += new ActiveObjects.SqlServer.SqlCommandExecutedDelegate(SqlDataUtility_SqlCommandExecuted);
        }

        static int totalQueries = 0;

        static void SqlDataUtility_SqlCommandExecuted(ActiveObjects.SqlServer.SqlCommandType type, string command, Dictionary<string, object> parameters)
        {
            int queries = totalQueries++;
            HttpContext context = HttpContext.Current;
            string url = context != null && context.Request != null ? context.Request.RawUrl : null;
            if (context != null && context.Items != null)
            {
                int queryCount = 0;
                if (context.Items.Contains("SqlServerQueryCount"))
                {
                    int.TryParse(context.Items["SqlServerQueryCount"].ToString(), out queryCount);
                    context.Items.Remove("SqlServerQueryCount");
                }
                queryCount++;
                context.Items.Add("SqlServerQueryCount", queryCount);
            }
            if (log.IsDebugEnabled)
            {
                log.DebugFormat(
                    "SQL|{4}|{0}|{1}|{2}|{3}", 
                    command, 
                    type, 
                    Mubble.Models.Tools.GetParametersString(parameters), 
                    totalQueries++,
                    url                    
                    );
            }
        }
    }
}
