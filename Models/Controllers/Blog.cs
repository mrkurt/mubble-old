using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects.SqlServer;

namespace Mubble.Models.Controllers
{
    [RequirePermissions(PermissionType.Create, "full", "draft", "publish", "post draft", "post publish", "post full")]
    [RequirePermissions(PermissionType.Edit, "full", "draft", "publish", "post draft", "post publish", "post full")]
    public class Blog : ControllerWithDiscussion
    {

        private ActiveObjects.DataManager dataManager = new SqlDataManager<Blog>();
        public override ActiveObjects.DataManager DataManager
        {
            get { return this.dataManager; }
            set { this.dataManager = value; }
        }

        public override CommentCollection GetComments(int page, Dictionary<string, string> parameters)
        {
            if (parameters.ContainsKey("Slug"))
            {
                Post p = Post.FindFirst(this.ID, parameters["Slug"]);

                if (p != null && p.Discussion != null)
                {
                    return p.Discussion.GetComments(page);
                }
            }
            return null;
        }

        public override Link BuildLink(RouteParameters parameters)
        {
            Link l = this.Url.Clone() as Link;
            string formatString = "/{0:0000}/{1:00}/{2:00}/{3}";
            Route r = this.GetRoute();
            if (parameters["Slug"] != null)
            {
                formatString = (r != null) ? r.FormatString : formatString;

            }
            else if (parameters["Day"] != null)
            {
                formatString = "/{0:0000}/{1:00}/{2:00}";
            }
            else if (parameters["Month"] != null)
            {
                formatString = "/{0:0000}/{1:00}";
            }
            else if (parameters["Year"] != null)
            {
                formatString = "/{0:0000}";
            }
            else
            {
                formatString = "";
            }

            l.Extra = string.Format(
                formatString,
                parameters.Get("Year", DateTime.Now.Year),
                parameters.Get("Month", DateTime.Now.Month),
                parameters.Get("Day", DateTime.Now.Day),
                parameters["Slug"]
            );


            return l;
        }

        #region Handler helpers
        public override List<QueryEngine.Content> GetContentList(string[] groups)
        {
            QueryEngine.Query q = new Mubble.Models.QueryEngine.Query();
            q.AddTerm("ContainerID", this.ID.ToString(), true, false);

            q.EndResultIndex = 20;

            return QueryEngine.Content.Query(q, groups);
        }
        #endregion
    }
}
