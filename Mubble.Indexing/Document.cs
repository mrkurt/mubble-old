using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Mubble.Indexing
{
    [XmlRoot(ElementName="document")]
    public class Document
    {
        private Schema schema;
        [XmlIgnore]
        public Schema Schema
        {
            set
            {
                this.schema = value;
                this.SchemaName = value.Name; 
                this.SchemaVersion = value.Version;
            }
            get
            {
                return schema ?? this.index.GetSchema(this.SchemaName, this.SchemaVersion);
            }
        }

        [XmlAttribute(AttributeName="schema.name")]
        public string SchemaName { get; set; }

        [XmlAttribute(AttributeName="schema.version")]
        public string SchemaVersion { get; set; }

        [XmlElement(ElementName="fields")]
        public FieldCollection Fields { get; set; }

        private Index index;
        [XmlIgnore]
        public Index Index { set { this.index = value; } }

        public Document() 
        {
            this.Fields = new FieldCollection();
        }

        public IEnumerable<Field> GetUniqueFields(Schema schema)
        {
            return schema.GetUniqueFields(this);
        }

    }
}
