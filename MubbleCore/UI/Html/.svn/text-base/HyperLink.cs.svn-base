using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;
using ActiveObjects;

namespace Mubble.UI.Html
{
    /// <summary>
    /// 
    /// </summary>
    public class HyperLink : System.Web.UI.WebControls.HyperLink, IControl
    {
        private string path;

        /// <summary>
        /// The controller path to link to
        /// </summary>
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        private string parameters;

        /// <summary>
        /// Controller parameters to include in the link.  For example: /2006/10/20/blah, resulting in
        /// http://domain.com/controller.aspx/2006/10/20/blah
        /// </summary>
        public string Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }


        private string handler = "HtmlHandler";

        /// <summary>
        /// The handler type to link to
        /// </summary>
        public virtual string Handler
        {
            get { return handler; }
            set { handler = value; }
        }

        private string anchor;

        public string Anchor
        {
            get { return anchor; }
            set { anchor = value; }
        }

		private int maxTitleLength = 0;

		/// <summary>
		/// Gets or sets the maximum length of an automatic title
		/// </summary>
		public int MaxTitleLength
		{
			get { return maxTitleLength; }
			set { maxTitleLength = value; }
		}


        private Link urlPair;

        /// <summary>
        /// The Link to generate a hyperlink tag from.
        /// </summary>
        public virtual Link UrlPair
        {
            get { return urlPair; }
            set { urlPair = value; }
        }

        private bool doLink = true;
        protected virtual bool DoLink
        {
            get { return doLink; }
            set { doLink = value; }
        }

        private string from;

        /// <summary>
        /// A string indicating which object in the scope to get a link from.  Normal strings, such as "Author" will look for
        /// and Author object.  If a string begins with # (like "#Author"), the control will look for an ancestor with an ID of "Author".
        /// </summary>
        public string From
        {
            get { return from; }
            set { from = value; }
        }


        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (DoLink)
            {
                if (this.Path != null)
                {
                    this.UrlPair = new Link(this.Path);
                }
                if ((this.UrlPair == null && string.IsNullOrEmpty(this.NavigateUrl)) || !string.IsNullOrEmpty(this.From))
                {
                    ILinkable scope = Control.GetCurrentScope<ILinkable>(this, this.From);
                    Link scoped = (scope != null) ? scope.Url : null;

                    if (scoped != null)
                    {
                        this.UrlPair = scoped;
                    }
                }

                if (this.UrlPair != null)
                {
                    this.NavigateUrl = MubbleUrl.Url(this.UrlPair, this.Handler);
                }

                if (!string.IsNullOrEmpty(this.NavigateUrl) && this.NavigateUrl[0] == '$')
                {
                    string field = this.NavigateUrl.Substring(1);
                    IActiveObject obj = Control.GetCurrentScope<IActiveObject>(this);
                    if (obj != null) this.NavigateUrl = obj.DataManager[field] as string;
                }

                if ((this.Text == null || this.Text.Trim().Length == 0) && this.Controls.Count == 0 && this.UrlPair != null)
                {
                    this.Text = this.CheckForMaxTitle(this.UrlPair.Title);
                }
            }
            if (this.Anchor != null && this.NavigateUrl != null) this.NavigateUrl += "#" + System.Web.HttpUtility.UrlEncode(this.Anchor);
            base.Render(writer);
        }

		protected string CheckForMaxTitle(string text)
		{
			if (this.MaxTitleLength > 0 && text.Length > this.MaxTitleLength)
			{
				int chopAfter = -1;
				bool lastWasDivider = false;
				for (int i = this.MaxTitleLength; i >= 0; i--)
				{
					char c = text[i];
					if (char.IsLetterOrDigit(c) && lastWasDivider)
					{
						chopAfter = i;
						break;
					}
					lastWasDivider = char.IsPunctuation(c) || char.IsSeparator(c);
				}
				if (chopAfter <= 0)
				{
					chopAfter = this.MaxTitleLength - 1;
				}
				text = text.Substring(0, chopAfter + 1) + "...";
			}
			return text;
		}

        #region IControl Members

        public PathParameters Params
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        private Page page;
        public new Page Page
        {
            get { if (page == null) page = base.Page as Page; return page; }
        }

        private Controller content;
        /// <summary>
        /// Gets or sets the content the URL should be built from
        /// </summary>
        public Controller Content
        {
            get
            {
                if (content == null)
                {
                    content = Control.GetCurrentScope<Controller>(this);
                }
                return content;
            }
            set { content = value; }
        }

        /// <summary>
        /// Gets the parsed URL associated with this request
        /// </summary>
        public MubbleUrl Url
        {
            get
            {
                return this.Page.Url;
            }
        }

        #endregion
    }
}
