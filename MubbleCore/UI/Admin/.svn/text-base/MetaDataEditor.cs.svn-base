using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;

namespace Mubble.UI.Admin
{
    public class MetaDataEditor : ActiveObjectFieldEditor
    {
        private MetaDataCollection metaData;

        public MetaDataCollection MetaData
        {
            get
            {
                if (metaData == null)
                {
                    if (this.ActiveObject != null)
                    {
                        HasMetadata md = this.ActiveObject.DataManager.ActsAs(typeof(HasMetadata)) as HasMetadata;
                        if (md != null)
                        {
                            metaData = this.ActiveObject.DataManager[md.FieldName] as MetaDataCollection;
                        }
                    }
                }
                return metaData;
            }
        }
    }
}
