using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models.QueryEngine
{
    public class ContentList : List<Content>, IEquatable<ContentList>
    {
        private int totalResults;

        public int TotalResults
        {
            get { return totalResults; }
            set { totalResults = value; }
        }

        private int startIndex;

        public int StartIndex
        {
            get { return startIndex; }
            set { startIndex = value; }
        }

        private int endIndex;

        public int EndIndex
        {
            get { return endIndex; }
            set { endIndex = value; }
        }


        #region IEquatable<ContentList> Members

        public bool Equals(ContentList other)
        {
            if (other == null) return false;
            if (other.Count != this.Count) return false;

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].ActiveObjectID != other[i].ActiveObjectID || !this[i].Type.Equals(other[i].Type))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}
