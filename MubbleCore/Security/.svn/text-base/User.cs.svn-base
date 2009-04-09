using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using ActiveObjects;
using Mubble.Models;
using System.Web;
using System.Web.Configuration;

namespace Mubble.Security
{
    public static class User
    {
        public static string[] GetRoles()
        {
            List<string> roles = new List<string>(Roles.GetRolesForUser());

            if(!roles.Contains("Anonymous"))
            {
                roles.Add("Anonymous");
            }
            return roles.ToArray();
        }

        public static bool HasFlag(IActiveObject ao, string flag)
        {
            PermissionsCollection permissions = GetPermissionSettings(ao);

            if (permissions == null) return false;

            return permissions.HasFlag(flag, GetRoles());
        }

        public static bool HasPermission(IActiveObject ao, params PermissionType[] types)
        {
            PermissionsCollection permissions = GetPermissionSettings(ao);

            if (permissions == null) return false;

            string[] groups = GetRoles();
            foreach (PermissionType type in types)
            {
                if (permissions.HasPermission(type, groups)) return true;
            }
            return false;
        }

        static PermissionsCollection GetPermissionSettings(IActiveObject ao)
        {
            LockedDown ld = ao.DataManager.ActsAs(typeof(LockedDown)) as LockedDown;

            if (ld == null) return null;

            return ao.DataManager[ld.FieldName] as PermissionsCollection;
        }

        public static void RedirectToLogin()
        {
            HttpContext context = HttpContext.Current;
            string loginUrl = MubbleUrl.Url(WebConfigurationManager.AppSettings["LoginContent"], "HtmlHandler");

            context.Response.Redirect(
                loginUrl + "?origin=" + context.Server.UrlEncode(context.Request.RawUrl)
            );
        }
    }
}
