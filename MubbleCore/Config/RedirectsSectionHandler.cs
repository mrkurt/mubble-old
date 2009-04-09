using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Xml;
using System.Text.RegularExpressions;

namespace Mubble.Config
{
    public class RedirectsSectionHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, XmlNode section)
        {
            //throw new Exception("The method or operation is not implemented.");
            Redirects r = new Redirects();
            foreach (XmlNode node in section.SelectNodes("redirect"))
            {
                string pattern = node.Attributes["pattern"].Value;
                if (pattern != null)
                {
                    r.Patterns.Add(new Regex(pattern, 
                        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace
                        | RegexOptions.ExplicitCapture)
                        );
                }
            }
            return r;
        }

        #endregion
    }
}
