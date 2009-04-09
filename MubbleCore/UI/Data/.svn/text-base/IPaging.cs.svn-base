using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;

namespace Mubble.UI.Data
{
    /// <summary>
    /// Specifies a set of behaviors for controls that have pages of data.  Used to create paging links, dropdowns, etc.
    /// </summary>
    public interface IPaging
    {
        List<Link> PageLinks { get; }
        Link NextPageLink { get; }
        Link PreviousPageLink { get; }
        Link CurrentPageLink { get; }
        bool HasPages { get; }
    }
}
