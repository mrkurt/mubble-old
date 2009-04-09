using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Mubble.Indexing
{
    public class Field
    {
        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }

        [XmlElement(ElementName="value")]
        public List<string> Values { get; set; }

        public void AddValues(Field field)
        {
            this.Values.AddRange(field.Values);
        }

        public Field()
        {
            this.Values = new List<string>();
        }

        public string GetValue()
        {
            if (this.Values.Count == 1)
            {
                return this.Values[0];
            }
            else
            {
                var sorted = from v in this.Values
                             orderby v
                             select v;

                return sorted.Join(" | ");
            }
        }

        public string GetSignature(Schema schema)
        {
            var values = GetValue();

            return string.Concat(schema.Name, ".[", this.Name, "]=", values);
        }
    }
}
