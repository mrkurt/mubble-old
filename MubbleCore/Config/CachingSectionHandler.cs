using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;

namespace Mubble.Config
{
    public class CachingSectionHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, XmlNode section)
        {
            Caching c = new Caching();
            c.StaticHost = section.Attributes["staticHost"] != null ? section.Attributes["staticHost"].Value : c.StaticHost;

            if (section.Attributes["enableOutputCaching"] != null)
            {
                bool enabled = c.EnableOutputCaching;

                bool.TryParse(section.Attributes["enableOutputCaching"].Value, out enabled);
                c.EnableOutputCaching = enabled;
            }

            if (section.Attributes["sharedObjectCacheTime"] != null)
            {
                int time = c.SharedObjectCacheTime;

                int.TryParse(section.Attributes["sharedObjectCacheTime"].Value, out time);
                c.SharedObjectCacheTime = time;
            }

            return c;
        }

        #endregion
    }
}
