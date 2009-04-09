using System;
using System.Collections.Generic;
using System.Text;

using ActiveObjects;

using Mubble.Models.QueryEngine;

namespace Mubble.Models
{
    public class IsIndexable : ActiveObjects.ActsAsAttribute
    {
        private Type optionsType = typeof(IndexingOptions);

        public Type OptionsType
        {
            get { return optionsType; }
            set { optionsType = value; }
        }

        public override bool Save(DataManager manager)
        {
            this.UpdateIndex(manager);
            return true;
        }

        protected void UpdateIndex(DataManager manager)
        {
            IndexingOptions options = (IndexingOptions)Activator.CreateInstance(this.OptionsType, new object[] { manager }) as IndexingOptions;
            if (options == null) options = new IndexingOptions(manager);
            if (!options.AllowIndexing) return;

            string documentId = string.Format("{0}-{1}", manager.Settings.BaseActiveObjectType.FullName, manager.PrimaryKeyValue);

            IndexDocument document = new IndexDocument();
            document.ID = documentId;

            document.AddField(FieldType.Keyword, "ActiveObjectsType", manager.Settings.ActiveObjectType.FullName);
            document.AddField(FieldType.Keyword, "ActiveObjectsID", manager.PrimaryKeyValue);

            if (options.Expires < DateTime.MaxValue)
            {
                document.Expires = options.Expires;
            }

            this.SetDocumentFields(options, manager, document);

            Engine.Index(document);
        }

        private void SetDocumentFields(IndexingOptions options, DataManager manager, IndexDocument doc)
        {
            if (options.UseCustomFields)
            {
                foreach (IndexField field in options.Fields)
                {
                    object obj = (field.ValueType == IndexFieldValueType.Literal) ? field.Value : manager[field.Value.ToString()];
                    if (obj == null) continue;
                    doc.AddField(field.Type, field.FieldName, obj);
                }
                //if (doc.Boost == 0 && options.Created > DateTime.MinValue)
                //{
                //    doc.Boost = (float)(options.Created.Year - 1990) + (8.3f * (float)options.Created.Month);
                //}
            }
            else
            {
                foreach (string field in manager.Keys)
                {
                    object value = manager[field];
                    if (value == null) continue;
                    if (value is string && ((string)value).Length <= 255)
                    {
                        doc.AddField(FieldType.Text, field, value);
                    }
                    else if (value is string)
                    {
                        doc.AddField(FieldType.TextUnStored, field, value);
                    }
                    else
                    {
                        doc.AddField(FieldType.Keyword, field, value);
                    }
                }
            }
            this.SetDocumentMetaDataFields(options, manager, doc);
            this.SetDocumentPermissions(options, manager, doc);
        }

        private void SetDocumentMetaDataFields(IndexingOptions options, DataManager manager, IndexDocument doc)
        {
            HasMetadata md = manager.ActsAs(typeof(HasMetadata)) as HasMetadata;
            if (md == null) return;

            MetaDataCollection metadata = manager[md.FieldName] as MetaDataCollection;

            if (metadata == null) return;

            Dictionary<string, double> tagHighestValues = new Dictionary<string, double>(StringComparer.CurrentCultureIgnoreCase);

            foreach (Tag t in metadata)
            {
                if (t.StringValueNormalized == null)
                {
                    doc.AddField(FieldType.Keyword, t.Name, t.NumericValue);
                }
                else
                {
                    if (!tagHighestValues.ContainsKey(t.Name))
                    {
                        tagHighestValues.Add(t.Name, 0f);
                        foreach (Tag weighted in metadata.Get(t.Name))
                        {
                            if (tagHighestValues[t.Name] < weighted.NumericValue)
                            {
                                tagHighestValues[t.Name] = weighted.NumericValue;
                            }
                        }
                    }

                    float highest = (float)tagHighestValues[t.Name];

                    float boost = (float)Math.Abs(t.NumericValue - highest);

                    if ("author".Equals(t.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        doc.AddField(FieldType.Keyword, t.Name, t.StringValue.ToLower());
                    }
                    else
                    {
                        doc.AddField(FieldType.Keyword, t.Name, t.StringValueNormalized.ToLower());
                    }

                    //doc.AddField(FieldType.Keyword, t.Name, t.StringValueNormalized.ToLower(), boost);
                }
            }
        }

        private void SetDocumentPermissions(IndexingOptions options, DataManager manager, IndexDocument doc)
        {
            LockedDown ld = manager.ActsAs(typeof(LockedDown)) as LockedDown;
            if (ld == null) return;

            PermissionsCollection permissions = manager[ld.FieldName] as PermissionsCollection;

            if (permissions == null) return;

            string[] groups = (options.GroupsWithViewPermissions != null) ? options.GroupsWithViewPermissions : 
                permissions.GroupsWithViewPermissions;

            Array.Sort<string>(groups);

            foreach (string group in groups)
            {
                doc.AddField(FieldType.Keyword, "GroupWithViewPermissions", group.ToLower());
            }

            groups = permissions.GroupsWithCreatePermissions;
            Array.Sort<string>(groups);
            foreach (string group in groups)
            {
                doc.AddField(FieldType.Keyword, "GroupWithCreatePermissions", group.ToLower());
            }

            groups = permissions.GroupsWithEditPermissions;
            Array.Sort<string>(groups);
            foreach (string group in groups)
            {
                doc.AddField(FieldType.Keyword, "GroupWithEditPermissions", group.ToLower());
            }
        }
    }
}
