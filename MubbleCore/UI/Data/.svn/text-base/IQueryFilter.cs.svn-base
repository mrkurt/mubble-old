using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models.QueryEngine;

namespace Mubble.UI.Data
{
    public interface IQueryFilter
    {
        IControl Parent { get; set; }
        Query BuildQuery(Query current);
        void GetVisibleIndexes(QueryResults results, List<int> visibleIndexes);
    }
}
