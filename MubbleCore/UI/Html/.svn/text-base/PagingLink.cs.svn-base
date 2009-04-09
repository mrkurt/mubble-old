using System;
using System.Collections.Generic;
using System.Text;
using Mubble.UI.Data;
using Mubble.Models;

namespace Mubble.UI.Html
{
    public enum PagingLinkMode
    {
        NextPage = 1,
        PreviousPage = 2
    }
    public class PagingLink : HyperLink
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

        private string pathExtraFormat;

        public string PathExtraFormat
        {
            get { return pathExtraFormat; }
            set { pathExtraFormat = value; }
        }


        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (this.PageLinkFor != null)
            {
                IPaging paging = this.NamingContainer.FindControl(this.PageLinkFor) as IPaging;
                if (paging == null)
                {
                    throw new ArgumentException(
                        string.Format(
                        "The control specified using PageLinkFor ({0}) is not valid.  This property must be set to the ID of "
                        + "a control that implements IPaging.",
                        this.PageLinkFor
                        )
                    );
                }
                Link p = this.UrlPair = (this.Mode == PagingLinkMode.NextPage) ? paging.NextPageLink : paging.PreviousPageLink;

                if (p == null && this.HideWhenNoPage)
                {
                    this.Visible = false;
                    return;
                }
                else if (p == null && this.NoPageClass != null && this.NoPageClass.Length > 0)
                {
                    this.CssClass = this.NoPageClass;
                }

                if (p != null && !string.IsNullOrEmpty(this.PathExtraFormat))
                {
                    p = this.UrlPair = new Link(p.Path, string.Format(this.PathExtraFormat, p.Title), p.Title);
                }

                if (p != null)
                {
                    this.Text = string.Format(this.Text, this.CheckForMaxTitle(p.Title));
                }
                base.Render(writer);
            }
        }
    }
}
