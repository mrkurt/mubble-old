using System;
using System.Collections.Generic;
using System.Text;
using Mubble.UI.WebControls;

namespace Mubble.UI.Admin
{
    public class PublishStatus : Image
    {
        private DateTime publishDate;

        public DateTime PublishDate
        {
            get { return publishDate; }
            set { publishDate = value; }
        }

        private Mubble.Models.PublishStatus status;

        public Mubble.Models.PublishStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        private string draftIcon = "images/icon_status_draft.gif";

        public string DraftIcon
        {
            get { return draftIcon; }
            set { draftIcon = value; }
        }

        private string publishedIcon = "images/icon_status_success.gif";

        public string PublishedIcon
        {
            get { return publishedIcon; }
            set { publishedIcon = value; }
        }

        private string scheduledIcon = "images/icon_status_future.gif";

        public string ScheduledIcon
        {
            get { return scheduledIcon; }
            set { scheduledIcon = value; }
        }


        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (this.PublishDate < DateTime.Now && this.Status == Mubble.Models.PublishStatus.Published)
            {
                this.Src = this.PublishedIcon;
                this.Alt = "Published";
            }
            else if (this.PublishDate > DateTime.Now && this.Status == Mubble.Models.PublishStatus.Published)
            {
                this.Src = this.ScheduledIcon;
                this.Alt = "Scheduled";
            }
            else if (this.Status == Mubble.Models.PublishStatus.Draft)
            {
                this.Src = this.DraftIcon;
                this.Alt = "Draft";
            }
            if (this.Src == null || this.Src.Length == 0 || this.Src.Equals("none", StringComparison.CurrentCultureIgnoreCase))
            {
                this.Visible = false;
            }
            else
            {
                base.Render(writer);
            }
        }
    }
}
