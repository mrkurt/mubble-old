using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using ActiveObjects;
using Mubble.Models.QueryEngine;

namespace Mubble.Models
{
    public class IndexingOptions
    {
        private DateTime expires = DateTime.MaxValue;

        public DateTime Expires
        {
            get { return expires; }
            set { expires = value; }
        }

        private DateTime created = DateTime.MinValue;

        public DateTime Created
        {
            get { return created; }
            set { created = value; }
        }

        private bool useCustomFields = false;

        public bool UseCustomFields
        {
            get { return useCustomFields; }
            set { useCustomFields = value; }
        }

        private bool allowIndexing = true;

        public bool AllowIndexing
        {
            get { return allowIndexing; }
            set { allowIndexing = value; }
        }

        private string[] groupsWithViewPermissions;

        public string[] GroupsWithViewPermissions
        {
            get { return groupsWithViewPermissions; }
            set { groupsWithViewPermissions = value; }
        }


        private Collection<IndexField> fields = new Collection<IndexField>();

        public Collection<IndexField> Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        protected void AddField(string name, FieldType type)
        {
            this.Fields.Add(new IndexField(name, name, type, IndexFieldValueType.Key));
        }

        protected void AddField(string fieldName, string keyName, FieldType type)
        {
            this.Fields.Add(new IndexField(fieldName, keyName, type, IndexFieldValueType.Key));
        }

        protected void AddValueField(string name, object value, FieldType type)
        {
            this.Fields.Add(new IndexField(name, value, type, IndexFieldValueType.Literal));
        }

        protected IndexingOptions() { }
        public IndexingOptions(DataManager manager) { }
    }

    public class IndexField
    {
        private object value;

        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        private string fieldName;

        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        private FieldType type = FieldType.Keyword;

        public FieldType Type
        {
            get { return type; }
            set { type = value; }
        }

        private IndexFieldValueType valueType = IndexFieldValueType.Literal;

        public IndexFieldValueType ValueType
        {
            get { return valueType; }
            set { valueType = value; }
        }

        public IndexField(string fieldName, object value, FieldType type, IndexFieldValueType valueType)
        {
            this.fieldName = fieldName;
            this.value = value;
            this.type = type;
            this.valueType = valueType;
        }
    }

    public enum IndexFieldValueType
    {
        Literal,
        Key
    }
}
