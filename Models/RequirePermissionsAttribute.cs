using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequirePermissionsAttribute : System.Attribute
    {
        private PermissionType permissionType;

        public PermissionType PermissionType
        {
            get { return permissionType; }
            set { permissionType = value; }
        }

        private string[] flags;

        public string[] Flags
        {
            get { return flags; }
            set { flags = value; }
        }

        public RequirePermissionsAttribute(PermissionType permissionType, params string[] flags)
        {
            this.PermissionType = permissionType;
            this.Flags = flags;
        }
    }

    public enum PermissionType
    {
        View = 1,
        Create = 2,
        Edit = 3
    }
}
