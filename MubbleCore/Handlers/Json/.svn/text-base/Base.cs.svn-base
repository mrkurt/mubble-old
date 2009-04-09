using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using System.Web.Security;
using System.Web;

namespace Mubble.Handlers.Json
{
    public abstract class Base : CachedHttpHandler
    {
        public string GetJsonOutput(object contents)
        {
            DataManagerConverter converter = new DataManagerConverter();
            return this.GetJsonOutput(contents, converter);
        }
        public string GetJsonOutput(object contents, JsonConverter converter)
        {
            JsonResponse resp = new JsonResponse(contents is Exception, contents);
            string output = JavaScriptConvert.SerializeObject(resp, converter);
            return output;
        }

        public string GetJsonException(string message)
        {
            return this.GetJsonOutput(new JsonException(message));
        }

        public void RenderJson(System.Web.HttpContext context, object contents)
        {           
            //context.Response.ContentType = "application/json";
            context.Response.Write(this.GetJsonOutput(contents));
        }

        protected override void AssertPermission(params string[] flags)
        {
            if (!this.UserHasPermission(flags))
            {
                HttpContext context = HttpContext.Current;
                string[] groups = Roles.GetRolesForUser();
                SecurityException ex = new SecurityException("The current user does not have permission to perform the requested action.");
                ex.RequiredFlags = flags;
                ex.CurrentFlags = this.Controller.Permissions.GroupFlags(groups);
                ex.CurrentGroups = groups;
               
                this.RenderJson(context, ex);
                context.Response.End();
            }
        }
    }
}
