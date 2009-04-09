using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Data
{
    public class PageNumber : FilterBase, IPostFilter
    {
        public int CurrentPageNumber
        {
            get { return this.GetPageNumber(); }
        }

        private string parameterName;

        public string ParameterName
        {
            get { return parameterName; }
            set { parameterName = value; }
        }

        private int parameterIndex = -1;

        public int ParameterIndex
        {
            get { return parameterIndex; }
            set { parameterIndex = value; }
        }

        private int pageSize = -1;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private int pageCount = -1;

        public int PageCount
        {
            get { return pageCount; }
            set { pageCount = value; }
        }


        private int pageNumber = -1;
        protected int GetPageNumber()
        {
            if (pageNumber > 0)
            {
                return pageNumber;
            }
            if (this.ParameterIndex >= 0)
            {
                pageNumber = this.Parent.Url.GetPathItem(this.ParameterIndex, 1);
            }
            else if (this.ParameterName != null)
            {
                string pvalue = this.Parent.Params[this.ParameterName];
                if (pvalue != null)
                {
                    int.TryParse(pvalue, out pageNumber);
                }
            }
            if (pageNumber <= 0) pageNumber = 1;
            return pageNumber;
        }

        public override void Before(Dictionary<string, object> parameters)
        {
            int count = this.PageSize > 0 ? this.PageSize : 20;
            int pnumber = this.GetPageNumber();

            int startIndex = (pnumber - 1) * count;
            int endIndex = startIndex + count;

            parameters.Add("RowIndex_start", startIndex);
            parameters.Add("RowIndex_end", endIndex + 1);
        }
    }
}
