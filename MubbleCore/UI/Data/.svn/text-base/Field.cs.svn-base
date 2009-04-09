using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using Mubble.Models;

namespace Mubble.UI.Data
{
    public enum TextDisplayMode
    {
        Short = 1,
        ExtendedOnly = 2,
        Full = 3
    }
    public class Field : Mubble.UI.Control
    {
		private bool isPath = false;

		public bool IsPath
		{
			get { return isPath; }
			set { isPath = value; }
		}

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string format;

        public string Format
        {
            get { return format; }
            set { format = value; }
        }

        private TextDisplayMode displayMode = TextDisplayMode.Full;

        /// <summary>
        /// Gets or sets the display mode to use for text that contains a &lt;more /&gt; tag
        /// </summary>
        public TextDisplayMode DisplayMode
        {
            get { return displayMode; }
            set { displayMode = value; }
        }


        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (this.Name != null)
            {
                object value = null;
                int activeObjectDepth = 0;
                IActiveObject obj = Control.GetCurrentScope<IActiveObject>(this, null, ref activeObjectDepth);
                if (obj != null)
                {
                    value = obj.DataManager[this.Name];
                }
                
                if (value != null)
                {
                    if (this.Format == null)
                    {
                        this.Format = "{0}";
                    }

                    string svalue = value as string;
                    if (svalue != null && svalue.Contains("<more />"))
                    {
                        string[] parts = svalue.Split(new string[] { "<more />" }, StringSplitOptions.None);
                        switch (this.DisplayMode)
                        {
                            case TextDisplayMode.Short:
                                value = parts[0];
                                break;
                            case TextDisplayMode.ExtendedOnly:
                                value = parts.Length > 1 ? parts[1] : value;
                                break;
                        }
                    }
                    else if (svalue != null && svalue.Contains("<more />"))
                    {
                        value = svalue.Replace("<more />", "");
                    }

					if (svalue != null && this.IsPath)
					{
						value = MubbleUrl.Create(svalue);
					}

                    writer.Write(format, value);
                }
            }
        }
    }
}
