using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using ActiveObjects;

namespace Mubble.Handlers
{
    public class Indexing : System.Web.IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(System.Web.HttpContext context)
        {
            context.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            if (System.Web.Security.Roles.IsUserInRole("Administrators"))
            {
                //January 1, 1753
                DateTime startDate = new DateTime(1753, 1, 1);
                DateTime endDate = DateTime.Now.AddYears(10);

                DateTime.TryParse(context.Request.QueryString["StartDate"], out startDate);
                DateTime.TryParse(context.Request.QueryString["EndDate"], out endDate);

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("RowIndex_start", 0);
                parameters.Add("RowIndex_end", -1);

                context.Response.Write("<h1>ControllerStatus</h1><div id=\"ControllerStatus\">0%</div>");

                if (context.Request.QueryString["NoNews"] != "true")
                {
                    context.Response.Write("<h1 style=\"margin-top: 40px;\">Post Status</h1><div id=\"PostStatus\">0%</div>");
                }

                context.Response.Flush();
                ActiveCollection<Controller> controllers = Controller.Find(parameters);
                parameters["RowIndex_end"] = controllers.TotalResults + 10;
                controllers = Controller.Find(parameters);
                string lastPercentage = "";
                for (int i = 0; i < controllers.Count; i++)
                {
                    Controller c = controllers[i];
                    c.DataManager["IgnoreDiscussionPlz"] = true;
                    c.Save();
                    string thisPercentage = string.Format("{0:0.0}%", ((double)i / (double)(controllers.Count - 1)) * 100.00);
                    if (!thisPercentage.Equals(lastPercentage))
                    {
                        lastPercentage = thisPercentage;
                        context.Response.Write(string.Format("<script>document.getElementById('ControllerStatus').innerHTML = '{0}';</script>\r\n", lastPercentage));
                        context.Response.Flush();
                    }
                }

                if (context.Request.QueryString["NoNews"] != "true")
                {
                    
                    int pageSize = 1000;
                    int startIndex = 0;
                    int endIndex = startIndex + pageSize;
                    parameters["RowIndex_start"] = startIndex;
                    parameters["RowIndex_end"] = endIndex;
                    int counter = 1;

                    ActiveCollection<Post> posts = null;

                    while ((posts = Post.Find(parameters)) != null && posts.Count > 0)
                    {
                        int totalPages = (int)Math.Ceiling((decimal)posts.TotalResults / (decimal)pageSize);
                        for (int i = 0; i < posts.Count; i++)
                        {
                            Post p = posts[i];
                            p.DataManager["IgnoreDiscussionPlz"] = true;
                            p.Save();
                            string thisPercentage = string.Format("Page {1} of {2} - {0:0.0}%", ((double)i / (double)(posts.Count - 1)) * 100.00, counter, totalPages);
                            if (!thisPercentage.Equals(lastPercentage))
                            {
                                lastPercentage = thisPercentage;
                                context.Response.Write(string.Format("<script>document.getElementById('PostStatus').innerHTML = '{0}';</script>\r\n", lastPercentage));
                                context.Response.Flush();
                            }
                        }
                        startIndex = endIndex;
                        endIndex = startIndex + pageSize;
                        parameters["RowIndex_start"] = startIndex;
                        parameters["RowIndex_end"] = endIndex;
                        counter++;
                    }
                }

                Mubble.Models.QueryEngine.Engine.CurrentEngine.Optimize();
            }
        }
        #endregion
    }
}
