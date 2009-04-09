using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mubble.Indexing
{
    public class SchemaPointer
    {
        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName="version")]
        public string Version { get; set; }

        public Schema GetSchema(Index index)
        {
            return index.GetSchema(this.Name, this.Version);
        }
    }
}
