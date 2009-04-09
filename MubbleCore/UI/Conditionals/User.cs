using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;

namespace Mubble.UI.Conditionals
{
    public class User : Conditional
    {
        private List<string> memberOf = new List<string>();

        public string MemberOf
        {
            set { this.memberOf.Add(value); }
        }

        protected override bool Test()
        {
            if (this.memberOf != null && this.memberOf.Count > 0)
            {
                foreach(string r in this.memberOf){
                    string role = r;
                    bool negate = role.Length > 0 && role[0] == '!';
                    if (negate)
                    {
                        role = role.Substring(1);
                    }
                    if (Roles.IsUserInRole(role) ^ negate)
                    {
                        return true;
                    }
                }
                return false;
            }
            return false;
        }
    }
}
