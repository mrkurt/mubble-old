using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models
{
    public class PermissionsCollection : ActiveObjects.ActiveCollection<Permission>
    {
        public IEnumerable<string> this[string group]
        {
            get
            {
                foreach (Permission p in this)
                {
                    if (p.Group.Equals(group, StringComparison.CurrentCultureIgnoreCase))
                    {
                        yield return p.Flag;
                    }
                }
            }
        }

        public string[] GroupFlags(string[] groups)
        {
            List<string> flags = new List<string>();

            List<string> groupList = new List<string>(groups);

            foreach (Permission p in this)
            {
                if (groupList.Contains(p.Group))
                {
                    flags.Add(p.Flag);
                }
            }
            return flags.ToArray();
        }

        private Dictionary<string, bool> viewPermissions = new Dictionary<string, bool>(StringComparer.CurrentCultureIgnoreCase);
        private Dictionary<string, bool> editPermissions = new Dictionary<string, bool>(StringComparer.CurrentCultureIgnoreCase);
        private Dictionary<string, bool> createPermissions = new Dictionary<string, bool>(StringComparer.CurrentCultureIgnoreCase);

        public bool HasFlag(string flag, params string[] groups)
        {
            List<string> groupList = new List<string>(groups);
            foreach (Permission p in this)
            {
                if (groupList.Contains(p.Group) && p.Flag.Equals(flag, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasPermission(PermissionType type, params string[] groups)
        {
            foreach (string group in groups)
            {
                if (this.HasPermission(type, group)) return true;
            }
            return false;
        }

        public bool HasPermission(PermissionType type, string group)
        {
            Dictionary<string, bool> permissions = this.viewPermissions;
            switch (type)
            {
                case PermissionType.Create:
                    permissions = this.createPermissions;
                    break;
                case PermissionType.Edit:
                    permissions = this.editPermissions;
                    break;
            }

            foreach (string flag in this[group])
            {
                if (permissions.ContainsKey(flag) && permissions[flag])
                {
                    return true;
                }
            }
            return false;
        }

        public void AddViewPermission(string flag, bool allowed)
        {
            this.AddPermissionToCollection(flag, allowed, this.viewPermissions);
        }

        public void AddPermissionToCollection(string flag, bool allowed, Dictionary<string, bool> collection)
        {
            if (!collection.ContainsKey(flag))
            {
                collection.Add(flag, allowed);
            }
            else
            {
                collection[flag] = allowed;
            }
        }

        public void AddPermissionsToCollection(Dictionary<string, bool> collection, params string[] flags)
        {
            foreach (string flag in flags)
            {
                this.AddPermissionToCollection(flag, true, collection);
            }
        }

        public void ResetViewPermissions(params string[] flags)
        {
            this.ResetPermissions(this.viewPermissions, flags);
        }

        public void ResetPermissions(PermissionType permissionType, params string[] flags)
        {
            switch (permissionType)
            {
                case PermissionType.View:
                    this.ResetPermissions(this.viewPermissions, flags);
                    break;
                case PermissionType.Create:
                    this.ResetPermissions(this.createPermissions, flags);
                    break;
                case PermissionType.Edit:
                    this.ResetPermissions(this.editPermissions, flags);
                    break;
            }
        }

        protected void ResetPermissions(Dictionary<string, bool> collection, params string[] flags)
        {
            collection.Clear();
            this.AddPermissionsToCollection(collection, flags);
        }

        public void CopyTo(PermissionsCollection permissions)
        {
            foreach (Permission p in this)
            {
                Permission np = new Permission();
                np.Group = p.Group;
                np.Flag = p.Flag;

                permissions.Add(np);
            }
        }

        public void Add(string group, string flag)
        {
            Permission p = new Permission();
            p.Group = group;
            p.Flag = flag;
            this.Add(p);
        }

        public void Remove(string group, string flag)
        {
            for (int i = 0; i < this.Count; i++)
            {
                Permission p = this[i];
                if (p.Group == group && p.Flag == flag)
                {
                    this.RemoveAt(i);
                }
            }
        }

        protected override void InsertItem(int index, Permission item)
        {
            foreach (Permission p in this)
            {
                if (p.Group.Equals(item.Group, StringComparison.CurrentCultureIgnoreCase) && p.Flag.Equals(item.Flag, StringComparison.CurrentCultureIgnoreCase))
                {
                    return;
                }
            }
            base.InsertItem(index, item);
        }

        public string[] GetGroupsWithPermissions(PermissionType permissionType)
        {
            switch (permissionType)
            {
                case PermissionType.View:
                    return this.FindGroupsInPermissionsCollection(this.viewPermissions);
                case PermissionType.Create:
                    return this.FindGroupsInPermissionsCollection(this.createPermissions);
                case PermissionType.Edit:
                    return this.FindGroupsInPermissionsCollection(this.editPermissions);
            }
            return new string[0];
        }

        public string[] GroupsWithViewPermissions
        {
            get { return this.FindGroupsInPermissionsCollection(this.viewPermissions); }
        }

        public string[] GroupsWithEditPermissions
        {
            get { return this.FindGroupsInPermissionsCollection(this.editPermissions); }
        }

        public string[] GroupsWithCreatePermissions
        {
            get { return this.FindGroupsInPermissionsCollection(this.createPermissions); }
        }

        protected string[] FindGroupsInPermissionsCollection(Dictionary<string, bool> permissions)
        {
            List<string> groups = new List<string>();

            foreach (string group in this.GroupsWithFlags)
            {
                bool canView = false;
                foreach (Permission p in this)
                {
                    if (!p.Group.Equals(group, StringComparison.CurrentCultureIgnoreCase)) continue;
                    if (!permissions.ContainsKey(p.Flag)) continue;
                    if (permissions.ContainsKey(p.Flag) && permissions[p.Flag]) canView = true;
                    if (permissions.ContainsKey(p.Flag) && !permissions[p.Flag])
                    {
                        canView = false;
                        break;
                    }
                }
                if (canView) groups.Add(group);
            }
            return groups.ToArray();
        }

        public string[] GroupsWithFlags
        {
            get
            {
                List<string> groups = new List<string>();
                foreach (Permission p in this)
                {
                    if (!groups.Contains(p.Group))
                    {
                        groups.Add(p.Group);
                    }
                }

                return groups.ToArray();
            }
        }
    }
}
