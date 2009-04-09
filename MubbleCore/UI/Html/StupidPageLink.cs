using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Mubble.Models;
using Mubble.UI.Data;

namespace Mubble.UI.Html
{
    public class StupidPageLink : System.Web.UI.WebControls.HyperLink
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

        private bool hideWhenNoPage = true;

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

                Link p = (this.Mode == PagingLinkMode.NextPage) ? paging.NextPageLink : paging.PreviousPageLink;

                if (p == null && this.HideWhenNoPage)
                {
                    this.Visible = false;
                    return;
                }

                if (p != null)
                {
                    string pageParam = "page=" + p.Title;
                    this.NavigateUrl = this.Context.Request.RawUrl;
                    this.NavigateUrl = Regex.Replace(this.NavigateUrl, @"(p|P)age\=(\d+)", pageParam);
                    if (!this.NavigateUrl.Contains("?"))
                    {
                        this.NavigateUrl += "?" + pageParam;
                    }
                    else if (!this.NavigateUrl.ToLower().Contains("page="))
                    {
                        this.NavigateUrl += "&" + pageParam;
                    }

                    if (!string.IsNullOrEmpty(this.Text))
                    {
                        this.Text = string.Format(this.Text, p.Title);
                    }
                }

                if (p != null || !this.HideWhenNoPage)
                {
                    this.CssClass = (p == null) ? this.NoPageClass : this.CssClass;
                    base.Render(writer);
                }
            }
        }
    }
}
