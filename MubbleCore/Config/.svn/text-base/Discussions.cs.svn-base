using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Mubble.Config
{
    public class Discussions : BaseSection<Discussions>, IConfigurationSectionHandler
    {
        private bool enabled = true;

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            if (section.Attributes["enabled"] != null)
            {
                bool enabled = this.Enabled;

                bool.TryParse(section.Attributes["enabled"].Value, out enabled);
                this.Enabled = enabled;
            }
            return this;
        }

        #endregion
    }
}
