using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;

namespace Mubble.Models
{
    public class HasMetadata : ActiveObjects.ActsAsAttribute
    {
        public override string FieldName
        {
            get { return (base.FieldName == null) ? "Metadata" : base.FieldName; }
            set { base.FieldName = value; }
        }

        MetaDataCollection data;

        public override bool Save(DataManager manager)
        {
            if (data != null)
            {
                foreach (Tag t in data)
                {
                    if (t.DataManager.IsDirty && manager.PrimaryKeyValue is Guid)
                    {
                        t.Type = manager.Settings.BaseActiveObjectType.FullName;
                        t.ActiveObjectID = (Guid)manager.PrimaryKeyValue;
                    }
                }
                return data.Save();
            }
            return true;
        }

        public override object GetValue(DataManager manager)
        {
            if (data == null && manager.PrimaryKeyValue is Guid)
            {
                data = Tag.Find(
                    manager.Settings.BaseActiveObjectType.FullName,
                    (Guid)manager.PrimaryKeyValue
                );
            }

            if (data == null)
            {
                data = new MetaDataCollection();
            }

            return data;

        }
    }
}
