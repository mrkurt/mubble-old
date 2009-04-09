using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models.QueryEngine;
using Mubble.Models.Controllers;
using Mubble.Models;
using Mubble.Handlers.Json;
using System.Net;
using System.Xml;

namespace Mubble.Handlers
{
    public class JsonHandler : Json.Base
    {

        private static Dictionary<string, DateTime> pendingRequests = new Dictionary<string, DateTime>(StringComparer.CurrentCultureIgnoreCase);
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override string VaryByCustom
        {
            get { return "($groups)"; }
        }

        public override string ProcessUncachedRequest(System.Web.HttpContext context)
        {
            switch (this.Url.GetPathItem(0, "m-list").ToLower())
            {
                case "m-comments":
                    return this.HandleDiscussion(context);
                default:
                    int count = this.Url.GetPathItem(1, 20);
                    log.DebugFormat("JSON request for {0}", context.Request.RawUrl);
                    this.AddCachedObjectDependency(this.Controller);
                    List<Content> content = this.Controller.GetContentList(Security.User.GetRoles());
                    string output = this.GetJsonOutput(Json.JsonContent.ConvertContentList(content, this.Url, count));
                    return output;
            }
        }

        protected string HandleDiscussion(System.Web.HttpContext context)
        {
            ControllerWithDiscussion controller = this.Controller as ControllerWithDiscussion;
            if (controller == null) return this.GetJsonException("No Data");
            if (!Config.Discussions.Current.Enabled) return this.GetJsonException("Comments disabled");
            int page = 1;
            if (context.Request.QueryString["p"] != null)
            {
                int.TryParse(context.Request.QueryString["p"], out page);
            }
            try
            {
                log.DebugFormat("Loading comments from {0}", controller.Discussion.DiscussionLink);
                CommentCollection comments = controller.GetComments(page, this.PathParts);
                if (comments != null && comments.Pages > comments.PageNumber)
                {
                    this.UseSlidingExpiration = true;
                }
                else
                {
                    this.UseSlidingExpiration = false;
                    this.CacheTime = 2;
                }

                return (comments != null) ? this.GetJsonOutput(comments, new CommentCollectionConverter())
                    : this.GetJsonException("No Data");
            }
            catch (DiscussionUnavailableException ex)
            {
                this.CacheTime = 2;
                return this.HandleDiscussionUnavailableException(ex);
            }
        }

        protected string HandleDiscussionUnavailableException(DiscussionUnavailableException ex)
        {
            WebException webex = ex.InnerException as WebException;

            if (webex != null)
            {
                switch (webex.Status)
                {
                    case WebExceptionStatus.Timeout:
                        return this.GetJsonOutput(new JsonCommentsTimeout(webex.Message));
                    default:
                        return this.GetJsonOutput(new JsonCommentsException(webex.Message));
                }
            }

            XmlException xmlex = ex.InnerException as XmlException;
            if (xmlex != null)
            {
                return this.GetJsonOutput(new JsonCommentsException(xmlex.Message));
            }

            if (ex.Message.Equals("Discussion unavailable."))
            {
                return this.GetJsonOutput(new JsonCommentsNoDiscussion(ex.Message));
            }

            if(ex.Message.Equals("Discussion request pending"))
            {
                return this.GetJsonOutput(new JsonCommentsTimeout(ex.Message));
            }

            return this.GetJsonException(ex.Message);
        }
    }
}
