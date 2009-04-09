using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mubble.Indexing
{
    [XmlRoot(ElementName="field")]
    public class SchemaField
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "indexed")]
        public bool Indexed { get; set; }
        [XmlAttribute(AttributeName = "stored")]
        public bool Stored { get; set; }
        [XmlAttribute(AttributeName = "compressed")]
        public bool Compressed { get; set; }
        [XmlAttribute(AttributeName = "multiValued")]
        public bool MultiValued { get; set; }
        [XmlAttribute(AttributeName = "required")]
        public bool Required { get; set; }
        [XmlAttribute(AttributeName = "tokenized")]
        public bool Tokenized { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "precision")]
        public string Precision { get; set; }

        [XmlAttribute(AttributeName = "unique")]
        public bool Unique { get; set; }

        public SchemaField()
        {
            this.Indexed = true;
            this.Stored = false;
            this.Compressed = false;
            this.MultiValued = false;
            this.Required = false;
            this.Tokenized = true;
            this.Type = "string";
        }
    }
}
