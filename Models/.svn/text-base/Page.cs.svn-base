using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{
    [IsIndexable]
	public partial class Page : IComparable<Page>, ILinkable
	{
        #region IComparable<Page> Members

        public int CompareTo(Page other)
        {
            return this.PageNumber.CompareTo(other.PageNumber);
        }

        #endregion

        #region ILinkable Members

        public Link Url
        {
            get { return new Link(this.Controller.Path, "/" + this.PageNumber.ToString(), this.Name); }
        }

        #endregion
    }
}
