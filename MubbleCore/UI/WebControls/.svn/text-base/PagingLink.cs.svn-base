using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.WebControls
{
    public enum PagingLinkMode
    {
        NextPage = 1,
        PreviousPage = 2
    }
    public class PagingLink : System.Web.UI.WebControls.HyperLink
    {
        private string pageLinkFor;

        public string PageLinkFor
        {
            get { return pageLinkFor; }
            set { pageLinkFor = value; }
        }

        private PagingLinkMode mode = PagingLinkMode.NextPage;

        /// <summary>
        /// Gets or sets the page link mode for this link
        /// </summary>
        public PagingLinkMode Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        private bool hideWhenNoPage = false;

        public bool HideWhenNoPage
        {
            get { return hideWhenNoPage; }
            set { hideWhenNoPage = value; }
        }

        private string noPageClass;

        public string NoPageClass
        {
            get { return noPageClass; }
            set { noPageClass = value; }
        }


	

        public override void DataBind()
        {
            if (this.PageLinkFor != null)
            {
                IPageable paging = this.NamingContainer.FindControl(this.PageLinkFor) as IPageable;
                if (paging == null)
                {
                    throw new ArgumentException(
                        string.Format(
                        "The control specified using PageLinkFor ({0}) is not valid.  This property must be set to the ID of "
                        + "a control that implements IPageable.",
                        this.PageLinkFor
                        )
                    );
                }
                PagePair p = (this.Mode == PagingLinkMode.NextPage) ? paging.NextPage : paging.PreviousPage;
                if (p != null)
                {
                    this.NavigateUrl = p.Link;
                    this.Text = (this.Text != null) ? string.Format(this.Text, p.Name) : p.Name;
                }
                else if (p == null && this.HideWhenNoPage)
                {
                    this.Visible = false;
                }
                else if (p == null && this.NoPageClass != null && this.NoPageClass.Length > 0)
                {
                    this.CssClass = this.NoPageClass;
                }
            }
            base.DataBind();
        }
    }
}
