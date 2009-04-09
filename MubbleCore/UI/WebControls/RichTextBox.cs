using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;

using Mubble.Models;

namespace Mubble.UI.WebControls
{
    /// <summary>
    /// A subclass of the System.Web.UI.TextBox control.  Always set to MultiLine and turns on WYSIWYG.
    /// </summary>
    public class RichTextBox : TextBox
    {
        private Mubble.UI.Page page;

        /// <summary>
        /// Gets the container page for this control
        /// </summary>
        new public Mubble.UI.Page Page
        {
            get
            {
                if (this.page == null)
                {
                    this.page = base.Page as Mubble.UI.Page;
                }
                return page;
            }
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
        }

        /// <summary>
        /// Gets the content for the current request
        /// </summary>
        public Controller Content
        {
            get
            {
                return this.Page.Controller;
            }
        }

        new protected TextBoxMode TextMode
        {
            get { return TextBoxMode.MultiLine; }
        }
        public RichTextBox()
            : base()
        {
            base.TextMode = TextBoxMode.MultiLine;
            this.PreRender += new EventHandler(RichTextBox_PreRender);
        }

        //protected override void Render(System.Web.UI.HtmlTextWriter writer)
        //{
        //    this.RenderBeginTag(writer);
        //    if (!string.IsNullOrEmpty(this.Text))
        //    {
        //        writer.
        //    }
        //    writer.Write(this.Text);
        //    this.RenderEndTag(writer);

        //}

        void RichTextBox_PreRender(object sender, EventArgs e)
        {
            if (this.RichTextEnabled)
            {
                // <script type="text/javascript" src="<%# (AdminTemplateBase + "jscripts/tinymce.js") %>"></script>
                string scriptLocation = Page.ResolveUrl(Page.Controller.Template.Path) + "jscripts/tinymce.js";
                if (!System.IO.File.Exists(Page.MapPath(scriptLocation)))
                {
                    AdminPage aPage = this.Page as AdminPage;
                    if (aPage != null)
                    {
                        scriptLocation = aPage.AdminTemplateBase + "jscripts/tinymce.js";
                    }
                }
                string cssLocation = Page.ResolveUrl(Page.Controller.Template.Path) + "stylesheets/content.css";
                string scriptBlock = "\r\n\t<script type=\"text/javascript\">";
                if (System.IO.File.Exists(Page.MapPath(cssLocation)))
                {
                    scriptBlock += string.Format(
                        "\r\n\tmceContentCss='{0}';",
                        cssLocation
                    );
                }
                else
                {
                    scriptBlock += "\r\n\tmceContentCss='';";
                }
                cssLocation = Page.ResolveUrl("~/jscripts/mceEditor.css");
                if (System.IO.File.Exists(Page.MapPath(cssLocation)))
                {
                    scriptBlock += string.Format(
                        "\r\n\tmceEditorCss='{0}';",
                        cssLocation
                        );
                }
                else
                {
                    scriptBlock += "\r\n\tmceEditorCss='';";
                }
                string popupLocation = this.Page.Url.ToString("AdminHandler", "/InsertImage?template=popup");
                scriptBlock += "\r\n\tmceMubbleImagePopup='" + popupLocation + "';";

                if (scriptBlock != "\r\n\t<script type=\"text/javascript\">")
                {
                    scriptBlock += "\r\n\t</script>";
                }
                scriptBlock += string.Format("\r\n\t<script type=\"text/javascript\" src=\"{0}\"></script>", scriptLocation);
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(string), "tinymce", scriptBlock);
            }
        }                
    }
}