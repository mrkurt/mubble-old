using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Text.RegularExpressions;
using Mubble.Models;

namespace Mubble.Web.Admin
{
    public partial class InsertImage : Mubble.UI.AdminPage
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.AssertPermission("full", "post full", "publish", "draft");
        }

        private string rawMediaUrl;

        /// <summary>
        /// Gets the raw URL specified for this media
        /// </summary>
        protected string RawMediaUrl
        {
            get { return rawMediaUrl; }
        }

        /// <summary>
        /// The base URL for mubble media
        /// </summary>
        protected string BaseMediaUrl
        {
            get
            {
                if (this.specifiedUrl != null)
                {
                    return this.specifiedUrl.ToString("MediaHandler", "");
                }
                else
                {
                    return null;
                }
            }
        }

        protected string MediaHost
        {
            get
            {
                if (!string.IsNullOrEmpty(Mubble.Config.Caching.Current.StaticHost))
                {
                    return Mubble.Config.Caching.Current.StaticHost;
                }
                else
                {
                    return Mubble.MubbleUrl.DefaultHost;
                }
            }
        }


        private bool isMubbleMedia=false;

        /// <summary>
        /// Gets a flag indicating if the specified media URL is a mubble URL.
        /// </summary>
        protected bool IsMubbleMedia
        {
            get { return isMubbleMedia; }
        }

        private Size mediaSize = new Size(0,0);

        /// <summary>
        /// Gets the size of the media, if set, otherwise the size is initialized to 0s.
        /// </summary>
        protected Size MediaSize
        {
            get { return mediaSize; }
        }

        private string mediaFileName;

        /// <summary>
        /// Gets the file name of the selected media.  If not mubble media, returns full URL.
        /// </summary>
        protected string MediaFileName
        {
            get { return mediaFileName; }
        }



        private MubbleUrl specifiedUrl;

        /// <summary>
        /// Gets the media URL, if any, passed to this page
        /// </summary>
        protected MubbleUrl SpecifiedUrl
        {
            get { return specifiedUrl; }
        }
	
        protected void Page_Load(object sender, EventArgs e)
        {
            string src = Request.QueryString["src"];
            string filter = Request.QueryString["filter"];
            if (Request.QueryString["view"] == "list" || (filter != null && filter.Trim().Length > 0))
            {
                this.SectionImageList.Visible = true;
            }
            else if (src != null && src.Trim().Length > 0)
            {
                src = Server.UrlDecode(src);
                this.specifiedUrl = MubbleUrl.Parse(src, "MediaHandler");
                if (this.specifiedUrl != null && this.specifiedUrl.PathExtra != null)
                {
                    this.ParseMediaUrl(this.specifiedUrl.PathExtra);
                    this.isMubbleMedia = true;
                }
                else
                {
                    this.mediaFileName = src;
                    this.isMubbleMedia = false;
                }
                this.rawMediaUrl = src;
                this.SectionImageOptions.Visible = true;
            }
            else
            {
                this.SectionSourceSelection.Visible = true;
            }

            this.SharedImageList.DataSource = Mubble.Models.Controller.RootContent.Files;
        }

        protected void ParseMediaUrl(string path)
        {
            path = Server.UrlDecode(path);
            this.mediaFileName = path.Substring(path.LastIndexOf('/'));

            int width = 0, height = 0;

            string parameterPattern = @"(?<type>\w){(?<value>\w+?)}";
            Regex r = new Regex(parameterPattern, RegexOptions.IgnoreCase);

            MatchCollection matches = r.Matches(path);

            bool sizeSet = false;
            foreach (Match m in matches)
            {
                switch (m.Groups["type"].Value.ToLower())
                {
                    case "w":
                        //sets width
                        int.TryParse(m.Groups["value"].Value, out width);
                        sizeSet = true;
                        break;
                    case "h":
                        //sets height
                        int.TryParse(m.Groups["value"].Value, out height);
                        sizeSet = true;
                        break;
                }
            }

            if (!sizeSet)
            {
                width = this.Url.GetPathItem(0, 0);
                height = this.Url.GetPathItem(1, 0);
            }

            this.mediaSize = new Size(width, height);
        }
        protected void UploadButton_click(object sender, System.EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                Mubble.Models.File m = new Mubble.Models.File();
                m.ControllerID = this.Controller.ID;
                m.FileName = fileUpload.FileName;
                m.Name = fileUpload.FileName;

                m.Save(fileUpload.FileContent);

                string imageLoc = Url.ToString("MediaHandler", "/" + m.FileName);
                string newLoc = Url.ToString("AdminHandler", Url.PathExtra + "?template=popup&src=" + imageLoc);
                Response.Redirect(newLoc);
            }
        }
    }
}
