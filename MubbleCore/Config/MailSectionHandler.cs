using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Mubble.Config
{
    public class MailSectionHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            Mail mail = new Mail();

            mail.StaffDomain = section.Attributes["staffDomain"] != null ? section.Attributes["staffDomain"].Value : mail.StaffDomain;

            return mail;
        }

        #endregion
    }
}
