using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models.QueryEngine;

namespace Mubble.UI.Data
{
    public class ControllerQuery : BaseQueryFilter
    {
        public override Query BuildQuery(Query current)
        {
            Mubble.Models.Controllers.Query q = this.Parent.Content as Mubble.Models.Controllers.Query;
            if (q != null)
            {
                current.Add(q.GetSpecifiedQuery());
            }
            return current;
        }
    }
}
