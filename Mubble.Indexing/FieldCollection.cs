using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Xml.Linq;
using System.Xml.Serialization;

namespace Mubble.Indexing
{
    public class FieldCollection
    {
        public Field this[string name]
        {
            get
            {
                if (fields.ContainsKey(name)) return fields[name];
                return null;
            }
            set
            {
                Set(value, true);
            }
        }

        private Field[] all = new Field[0];

        [XmlElement(ElementName="field")]
        public Field[] All
        {
            get { return all; }
            set 
            {
                LoadDictionary(all);
            }
        }

        public void Add(Field value)
        {
            Set(value, true);
        }

        public void Replace(Field value)
        {
            fields.Remove(value.Name);
            Set(value, false);
            for (int i = 0; i < all.Length; i++)
            {
                if (all[i].Name == value.Name)
                {
                    all[i] = value;
                    return;
                }
            }
        }

        private void Set(Field value, bool setArray)
        {
            if (fields.ContainsKey(value.Name)) fields[value.Name].AddValues(value);
            else
            {
                if (setArray)
                {
                    var a = new List<Field>(all);
                    a.Add(value);
                    all = a.ToArray();
                }
                fields.Add(value.Name, value);
            }
        }

        private void LoadDictionary(Field[] list)
        {
            fields.Clear();
            foreach (var f in list)
            {
                this.Set(f, false);
            }
            this.all = fields.Values.ToArray();
        }

        private readonly Dictionary<string, Field> fields;

        public FieldCollection()
        {
            this.fields = new Dictionary<string, Field>(StringComparer.CurrentCultureIgnoreCase);
        }
    }
}
