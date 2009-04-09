using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;

namespace Mubble.Models
{
    public class LockedDown : ActiveObjects.ActsAsAttribute
    {

        private string usePermissionsFrom;

        public string UsePermissionsFrom
        {
            get { return usePermissionsFrom; }
            set { usePermissionsFrom = value; }
        }


        private string[] viewPermissions = new string[0];

        public override string FieldName
        {
            get { return (base.FieldName == null) ? "Permissions" : base.FieldName; }
            set { base.FieldName = value; }
        }

        PermissionsCollection data;

        public LockedDown() { }

        public LockedDown(params string[] viewPermissions)
        {
            this.viewPermissions = viewPermissions;
        }

        public override bool Save(DataManager manager)
        {
            if (data != null)
            {
                foreach (Permission p in data)
                {
                    if (p.DataManager.IsDirty && manager.PrimaryKeyValue is Guid)
                    {
                        p.Type = manager.Settings.BaseActiveObjectType.FullName;
                        p.ActiveObjectID = (Guid)manager.PrimaryKeyValue;
                    }
                }
                return data.Save();
            }
            return true;
        }

        public override object GetValue(DataManager manager)
        {
            bool loadPermissionAttributes = false;
            if (data == null && this.UsePermissionsFrom == null && manager.PrimaryKeyValue is Guid)
            {
                loadPermissionAttributes = true;
                data = Permission.Find(
                    manager.Settings.BaseActiveObjectType.FullName,
                    (Guid)manager.PrimaryKeyValue
                );

                foreach (string perm in this.viewPermissions)
                {
                    data.AddViewPermission(perm, true);
                }

            }
            else if (data == null && this.UsePermissionsFrom != null && manager[this.UsePermissionsFrom] is IActiveObject)
            {
                loadPermissionAttributes = true;
                IActiveObject parent = (IActiveObject)manager[this.UsePermissionsFrom];
                LockedDown attrib = parent.DataManager.ActsAs(typeof(LockedDown)) as LockedDown;

                if (attrib != null)
                {
                    data = parent.DataManager[attrib.FieldName] as PermissionsCollection;
                }
                foreach (string perm in this.viewPermissions)
                {
                    data.AddViewPermission(perm, true);
                }
            }
            if (loadPermissionAttributes)
            {
                Attribute[] attributes = Attribute.GetCustomAttributes(manager.Settings.ActiveObjectType, typeof(RequirePermissionsAttribute));
                foreach(RequirePermissionsAttribute att in attributes)
                {
                    data.ResetPermissions(att.PermissionType, att.Flags);
                }
            }
            return data;
        }
    }
}
