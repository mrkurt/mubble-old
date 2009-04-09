using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mubble.Data.Mapper
{
    public static class DataObjectHelper
    {

        public static TypeCollection Convert(string path)
        {
            var doc = XDocument.Load(path);

            return Convert(doc);
        }

        public static TypeCollection Convert(XDocument doc)
        {
            var types = from node in doc.Descendants("type")
                        select new DataType
                        {
                            Name = node.Attribute("name").Value,
                            Namespace = node.Attribute("namespace").Value,
                            Properties = GetProperties(node)
                        };

            types.ToList().ForEach(a => Console.WriteLine(a.Namespace + "." + a.Name));

            return new TypeCollection { Types = types.ToList() };
        }

        static List<Property> GetProperties(XElement node)
        {
            var props = from n in node.Elements("property")
                        select new Property
                        {
                            Name = n.Attribute("name").Value,
                            Type = n.Attribute("type").Value,
                            IsInherited = bool.Parse(n.Attribute("inherited").Value)
                        };

            return props.ToList();
        }

    }
}
