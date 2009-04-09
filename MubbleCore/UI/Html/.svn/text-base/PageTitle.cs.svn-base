using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.UI.Html
{
    public class PageTitle : Control
    {
        private string format;

        public string Format
        {
            get { return format; }
            set { format = value; }
        }

        private string value;

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }


        public PageTitle()
        {
            this.Load += new EventHandler(PageTitle_Load);
        }

        void PageTitle_Load(object sender, EventArgs e)
        {
            string value = Control.GetReferencedValue(this, this.Value);
            if (value != null)
            {
                if (this.Format != null) value = string.Format(this.Format, value);
                Page.Title = value;
            }
        }
    }
}
