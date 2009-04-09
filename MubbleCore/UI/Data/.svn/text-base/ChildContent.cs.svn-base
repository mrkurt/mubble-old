using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;

namespace Mubble.UI.Data
{
    public class ChildContent : BaseQueryFilter
    {
        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public override Mubble.Models.QueryEngine.Query BuildQuery(Mubble.Models.QueryEngine.Query current)
        {
            Controller c = null;
            if (string.IsNullOrEmpty(this.Path))
            {
                c = Control.GetCurrentScope<Controller>(this.Parent.Parent);
            }
            else
            {
                c = DataBroker.GetController(this.Path);
            }

            if (c != null)
            {
                this.Path = c.Path;
                current.AddTerm("Path", this.Path + "*", true, true, false);
                current.AddTerm("ActiveObjectsID", c.ID.ToString(), false, true);
            }
            return current;
        }
    }
}
