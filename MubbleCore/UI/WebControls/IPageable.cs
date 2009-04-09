using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.WebControls
{
    /// <summary>
    /// Defines the properties and methods that must be implemented by controls to be considered pageable.
    /// </summary>
    public interface IPageable
    {
        /// <summary>
        /// Gets the currently selected page of data.
        /// </summary>
        PagePair CurrentPage { get; }
        /// <summary>
        /// Gets the next page, if available.  Null if no page.
        /// </summary>
        PagePair NextPage { get; }
        /// <summary>
        /// Gets the previous page, if available.  Null if no page.
        /// </summary>
        PagePair PreviousPage { get; }
        /// <summary>
        /// Gets all available pages.
        /// </summary>
        List<PagePair> AllPages { get; }

        /// <summary>
        /// Gets a flag indicating whether paging controls will have any effect
        /// </summary>
        bool IsPageable { get;}
    }
    public class PagePair
    {
        public string Name;
        public string Link;
    }
}
