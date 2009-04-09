using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mubble.Data.Tests
{
    public class Serializer
    {
        public static string Serialize<T>(ContentItemBase<T> content) where T : ContentItemBase<T>, new()
        {
            var xml = new XElement(content.GetType().Name);

            var map = content.Map;

            foreach (var key in map.GetKeys())
            {
                var value = content[key];
                var n = new XElement(key, GetStringFor(value));

                n.Add(new XAttribute("Type", value.GetType().Name));

                xml.Add(n);
            }

            return xml.ToString();
        }

        static string GetStringFor(object obj)
        {
            if (obj is DateTime)
            {
                return ((DateTime)obj).ToUniversalTime().ToString();
            }
            return obj.ToString();
        }
    }
}