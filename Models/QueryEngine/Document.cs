using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using ActiveObjects;

namespace Mubble.Models.QueryEngine
{
    public class IndexDocument
    {
        public static string GetObjectID(IActiveObject obj)
        {
            DataManager manager = obj.DataManager;
            return string.Format("{0}-{1}", manager.Settings.ActiveObjectType.FullName, manager.PrimaryKeyValue);
        }

        private string id;

        public string ID
        {
            get { return id; }
            set { id = value.ToLower(); }
        }


        private DateTime expires = DateTime.MaxValue;

        public DateTime Expires
        {
            get { return expires; }
            set { expires = value; }
        }

        private float score;

        public float Score
        {
            get { return score; }
            set { score = value; }
        }

        private float boost;

        public float Boost
        {
            get { return boost; }
            set { boost = value; }
        }


        private int indexID;

        public int IndexID
        {
            get { return indexID; }
            set { indexID = value; }
        }


        private Collection<IndexField> fields = new Collection<IndexField>();

        public Collection<IndexField> Fields
        {
            get { return fields; }
        }

        public string Get(string field)
        {
            IndexField f = this.GetField(field);
            if (f != null)
            {
                return f.Value.ToString();
            }
            return null;
        }

        public DateTime GetDate(string field)
        {
            IndexField f = this.GetField(field);
            DateTime date = DateTime.MinValue;
            if (f != null)
            {
                date = Engine.DateValue(f);
            }
            return date;
        }

        public IndexField GetField(string field)
        {
            foreach (IndexField f in this.Fields)
            {
                if (f.Name.Equals(field, StringComparison.CurrentCultureIgnoreCase))
                {
                    return f;
                }
            }
            return null;
        }

        public string[] GetValues(string field)
        {
            List<string> values = new List<string>();
            foreach (IndexField f in this.Fields)
            {
                if (f.Name.Equals(field, StringComparison.CurrentCultureIgnoreCase))
                {
                    values.Add(f.Value.ToString());
                }
            }
            return values.ToArray();
        }

        public void AddField(FieldType type, string name, object value)
        {
            this.AddField(type, name, value, 0);   
        }

        public void AddField(FieldType type, string name, object value, float boost)
        {
            this.AddField(new IndexField(type, name, value, boost));
        }

        public void AddField(IndexField field)
        {
            this.Fields.Add(field);
        }
    }
}
