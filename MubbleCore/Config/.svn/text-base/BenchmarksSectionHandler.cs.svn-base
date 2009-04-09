using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Xml;

namespace Mubble.Config
{
    public class BenchmarksSectionHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, XmlNode section)
        {
            Benchmarks b = new Benchmarks();
            if (section.Attributes["enabled"] != null)
            {
                bool enabled = b.Enabled;
                bool.TryParse(section.Attributes["enabled"].Value, out enabled);
                b.Enabled = enabled;
            }

            if (section.Attributes["minTime"] != null)
            {
                int minTime = b.MinTime.Milliseconds;
                int.TryParse(section.Attributes["minTime"].Value, out minTime);
                b.MinTime = TimeSpan.FromMilliseconds(minTime);
            }

            if (section.Attributes["queriesToTriggerLog"] != null)
            {
                int queries = b.QueriesToTriggerLog;
                int.TryParse(section.Attributes["queriesToTriggerLog"].Value, out queries);
                b.QueriesToTriggerLog = queries;
            }

            return b;
        }

        #endregion
    }
}
