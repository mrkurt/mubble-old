using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Data
{
    public class AuthorFilter : BaseQueryFilter
    {
        public override Mubble.Models.QueryEngine.Query BuildQuery(Mubble.Models.QueryEngine.Query current)
        {
            string author = this.Parent.Url.GetPathItem(0, "---");
            if (author != "---")
            {
                current.AddTerm("Author", author, true, false);
            }
            else
            {
                current.IsValid = false;
            }
            return current;
        }
    }
}
