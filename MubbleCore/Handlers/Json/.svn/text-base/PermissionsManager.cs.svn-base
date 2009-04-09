using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;

namespace Mubble.Handlers.Json
{
    public class PermissionsManager : Base
    {
        public override bool AllowCaching
        {
            get
            {
                return false;
            }
            set
            {
                base.AllowCaching = value;
            }
        }
        public override string ProcessUncachedRequest(System.Web.HttpContext context)
        {
            this.AssertPermission("full");

            switch (this.Url.GetPathItem(0, "").ToLower())
            {
                case "update":
                    string group = context.Request["Group"];
                    string flag = context.Request["Flag"];
                    bool enabled = "true".Equals(context.Request["Enabled"], StringComparison.CurrentCultureIgnoreCase);
                    if (enabled)
                    {
                        this.Controller.Permissions.Add(group, flag);
                    }
                    else
                    {
                        this.Controller.Permissions.Remove(group, flag);
                    }
                    this.Controller.Save();
                    break;
            }

            return this.GetJsonOutput(this.Controller.Permissions);
        }
    }
}
