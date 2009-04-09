using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Mubble.Config
{
    public class Cluster : BaseSection<Cluster>, IConfigurationSectionHandler
    {
        public bool Enabled { get { return !string.IsNullOrEmpty(LocalQueue); } }
        public string LocalQueue { get; set; }
        public string MulticastAddress { get; set; }

        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            this.LocalQueue = this.GetValue(section, "localQueue");
            //234.234.234.234:8001
            this.MulticastAddress = this.GetValue(section, "multicastAddress");
            return this;
        }

        #endregion
    }
}
