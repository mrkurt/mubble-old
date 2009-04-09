using System;
using System.Collections.Generic;
using System.Text;

namespace Mubble.Models.QueryEngine
{
    public class IndexField
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private object value;

        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private FieldType type = FieldType.Keyword;

        public FieldType Type
        {
            get { return type; }
            set { type = value; }
        }

        private float boost;

        public float Boost
        {
            get { return boost; }
            set { boost = value; }
        }


        public IndexField() { }

        public IndexField(FieldType type, string name, object value)
        {
            this.Type = type;
            this.Name = name;
            this.Value = value;
        }

        public IndexField(FieldType type, string name, object value, float boost) :
            this(type, name, value)
        {
            this.Boost = boost;
        }

    }
}
