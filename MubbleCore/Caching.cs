using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.Caching;
using System.IO;

namespace Mubble
{
    public static class Caching
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Init()
        {

        }
        public static string VaryByCustom(System.Web.HttpContext context, string custom)
        {
            custom = custom.ToLower();
            Dictionary<string, string> keys = new Dictionary<string, string>();

            string[] groups = Mubble.Security.User.GetRoles();
            Array.Sort(groups);

            keys.Add("groups", string.Join("-", groups));
            keys.Add("url", context.Request.RawUrl);
            keys.Add("query", context.Request.Url.Query);

            foreach (KeyValuePair<string, string> pair in keys)
            {
                custom = custom.Replace(string.Concat("$",pair.Key), pair.Value);
            }
            return custom.ToLower();
        }
    }
}
