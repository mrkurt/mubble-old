using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mubble.Indexing
{
    [XmlRoot(ElementName="schema")]
    public class Schema
    {
        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }

        private Version version;
        [XmlAttribute(AttributeName = "version")]
        public string Version
        {
            get 
            { 
                return this.version.ToString(); 
            }
            set
            {
                this.version = new Version(value);
            }
        }

        [XmlAttribute(AttributeName = "defaultSearchField")]
        public string DefaultSearchField { get; set; }

        [XmlArray(ElementName="fields")]
        [XmlArrayItem(ElementName="field")]
        public List<SchemaField> Fields { get; set; }

        public Schema() { }
        public Schema(string name, string version) : this()
        {
            this.Name = name;
            this.Version = version;
            this.Fields = new List<SchemaField>();
        }

        public IEnumerable<Field> GetUniqueFields(Document doc)
        {
            var blah = from f in doc.Fields.All
                       join sf in this.Fields on f.Name equals sf.Name
                       where sf.Unique
                       orderby f.Name
                       select f;

            return blah;
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                throw new SchemaException("The schema must have a name", new ArgumentNullException("Name"));
            }
        }
    }
}
