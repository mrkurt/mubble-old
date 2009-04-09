using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Xml;

namespace Mubble.Config
{
    public class IndexSectionHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, XmlNode section)
        {
            Index i = new Index();
            if (section.Attributes["useRamSearcher"] != null)
            {
                bool useRamSearcher = i.UseRamSearcher;
                bool.TryParse(section.Attributes["useRamSearcher"].Value, out useRamSearcher);
                i.UseRamSearcher = useRamSearcher;
            }

            if (section.Attributes["enabled"] != null)
            {
                bool enabled = i.Enabled;
                bool.TryParse(section.Attributes["enabled"].Value, out enabled);
                i.Enabled = enabled;
            }

            if (section.Attributes["maxTimeForQueryRefresh"] != null)
            {
                int maxTime = i.MaxTimeForQueryRefresh.Seconds;
                int.TryParse(section.Attributes["maxTimeForQueryRefresh"].Value, out maxTime);
                i.MaxTimeForQueryRefresh = TimeSpan.FromSeconds(maxTime);
            }
            return i;
        }

        #endregion
    }
}
