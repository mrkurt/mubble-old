using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web;

namespace Mubble.UI.WebControls
{
    public class RichTextBoxToggle : LinkButton
    {
        private string disableText = "Disable WYSIWYG";

        /// <summary>
        /// Gets or sets the text prompt to disable to the RichTextBoxes.
        /// </summary>
        public string DisableText
        {
            get { return disableText; }
            set { disableText = value; }
        }

        private string enableText = "Enable WYSIWYG";

        /// <summary>
        /// Gets or sets the text prompt to enable the RichTextBoxes.
        /// </summary>
        public string EnableText
        {
            get { return enableText; }
            set { enableText = value; }
        }

        /// <summary>
        /// Gets a flag indicating the current user's RichTextBox setting
        /// </summary>
        public bool RichTextEnabled
        {
            get
            {
                HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get("mubblewysiwyg");
                return (cookie == null || cookie.Value != "off");
            }
            set
            {
                HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get("mubblewysiwyg");
                if (cookie == null)
                {
                    cookie = new HttpCookie("mubblewysiwyg");
                }
                cookie.Value = (value) ? "on" : "off";
                cookie.Expires = DateTime.Now.AddYears(1);
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public RichTextBoxToggle() : base()
        {
            this.Click += new EventHandler(RichTextBoxToggle_Click);
            this.PreRender += new EventHandler(RichTextBoxToggle_PreRender);
        }

        void RichTextBoxToggle_PreRender(object sender, EventArgs e)
        {
            if (this.RichTextEnabled)
            {
                this.Text = DisableText;
            }
            else
            {
                this.Text = EnableText;
            }
        }

        void RichTextBoxToggle_Click(object sender, EventArgs e)
        {
            this.RichTextEnabled = !this.RichTextEnabled;
        }
    }
}
