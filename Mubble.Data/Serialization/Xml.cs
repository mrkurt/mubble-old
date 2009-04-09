using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mubble.Data.Serialization
{
    public class Xml
    {
        public static XElement Serialize<T>(List<T> content) where T : IData
        {
            var xml = new XElement("Items");

            foreach (var ci in content)
            {
                var cix = Serialize(ci);
                xml.Add(cix);
            }

            return xml;
        }

        public static XElement Serialize(IData content)
        {
            var xml = new XElement(content.GetType().Name);

            var map = Mubble.Data.Mapping.PropertyMap.GetMap(content.GetType());

            foreach (var key in map.GetKeys())
            {
                var value = content[key];
                var n = new XElement(key, GetStringFor(value));

                var type = value != null ? value.GetType().Name : null;
                if (type != null)
                {
                    n.Add(new XAttribute("Type", value.GetType().Name));
                }

                xml.Add(n);
            }

            if (content is Section)
            {
                var section = (Section)(object)content;

                xml.Add(
                    Serialize(section.List(DateTime.Now.AddYears(-2), DateTime.Now))
                    );
            }

            return xml;
        }

        static string GetStringFor(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            if (obj is DateTime)
            {
                return ((DateTime)obj).ToUniversalTime().ToString();
            }
            return obj.ToString();
        }
    }
}
