using System;
using System.Collections.Generic;
using System.Text;

using Mubble.Models;

namespace Mubble.UI.Html
{
    public class PublishStatus : Image
    {
        private string draftIcon;

        public string DraftIcon
        {
            get { return draftIcon; }
            set { draftIcon = value; }
        }

        private string publishedIcon;

        public string PublishedIcon
        {
            get { return publishedIcon; }
            set { publishedIcon = value; }
        }

        private string scheduledIcon;

        public string ScheduledIcon
        {
            get { return scheduledIcon; }
            set { scheduledIcon = value; }
        }

        public PublishStatus()
        {
            this.Load += new EventHandler(PublishStatus_Load);
        }

        void PublishStatus_Load(object sender, EventArgs e)
        {
            IContent obj = Control.GetCurrentScope<IContent>(this);
            if (obj == null)
            {
                this.Visible = false;
                return;
            }

            if (obj.Status == Mubble.Models.PublishStatus.Published && obj.PublishDate <= DateTime.Now)
            {
                this.Src = this.PublishedIcon;
                this.Alt = "Published";
            }
            else if (obj.Status == Mubble.Models.PublishStatus.Published)
            {
                this.Src = this.ScheduledIcon;
                this.Alt = "Scheduled for future";
            }
            else
            {
                this.Src = this.DraftIcon;
                this.Alt = "Draft";
            }
            if (string.IsNullOrEmpty(this.Src)) this.Visible = false;
        }
    }
}
