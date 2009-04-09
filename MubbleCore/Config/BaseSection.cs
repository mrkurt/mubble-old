using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Mubble.Config
{
    public abstract class BaseSection<T> where T : class, new()
    {
        public static T Current
        {
            get
            {
                T r = ConfigurationManager.GetSection(typeof(T).Name.ToLower()) as T;
                if (r == null) r = new T();
                return r;
            }
        }

        protected string GetValue(System.Xml.XmlNode section, string attribute)
        {
            if (section == null || section.Attributes[attribute] == null) return null;

            return section.Attributes[attribute].Value;
        }
    }
}
