using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Xml;

namespace Mubble.Config
{
    public class AdministrationSectionHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, XmlNode section)
        {
            Administration a = new Administration();
            a.TemplatePath = section.Attributes["templatePath"] != null ? section.Attributes["templatePath"].Value : a.TemplatePath;
            a.TemplateFileName = section.Attributes["templateFileName"] != null ? section.Attributes["templateFileName"].Value : a.TemplateFileName;

            if (section.Attributes["newStyle"] != null)
            {
                bool newStyle = false;
                bool.TryParse(section.Attributes["newStyle"].Value, out newStyle);
                a.NewStyle = newStyle;
            }
            return a;
        }

        #endregion
    }
}
