using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mubble.Data.Serialization
{
    public class Description : ISerializer
    {
        XElement root = new XElement("description");

        public override string ToString()
        {
            return root.ToString();
        }

        #region ISerializer Members

        public void Append(string field, object data, TypeInfo type)
        {
            var e = new XElement(
                "field",
                new XAttribute("name", field),
                new XAttribute("type", type.Name)
                );

            root.Add(e);
        }

        public void Start(Type baseType)
        {
            root.Add(new XAttribute("type", baseType.Name));
        }

        #endregion
    }
}
