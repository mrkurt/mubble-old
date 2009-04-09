using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mubble.Web.Output
{
    public class Documentation : ISerializer
    {
        XElement root;
        XElement current;

        public Documentation()
        {
            root = new XElement("type");
            current = root;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        #region ISerializer Members

        public void Start(Mubble.Data.IData data, TypeInfo type)
        {
            root.Add(new XAttribute("type", type.Name));
        }

        public void AppendField(string field, object data, TypeInfo type)
        {
            current.Add(
                new XElement(
                    "field",
                    new XAttribute("name", field),
                    new XAttribute("type", type.Name)
                    )
            );
        }

        public void StartRelated(string name, Mubble.Data.IData data, TypeInfo type)
        {
            var c = new XElement("related",
                        new XAttribute("name", name),
                        new XAttribute("type", type.Name)
                        );
            current.Add(c);
            current = c;
        }

        public void EndBlock()
        {
            current = current.Parent;
        }

        #endregion
    }
}
