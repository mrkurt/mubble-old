using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using Mubble.Models.QueryEngine;

namespace Mubble.Models
{
    public class ControllerIndexingOptions : IndexingOptions
    {
        public ControllerIndexingOptions(DataManager manager)
        {
            Controller c = Activator.CreateInstance(manager.Settings.ActiveObjectType) as Controller;
            c.DataManager = manager;

            this.UseCustomFields = true;
            this.AddField("Title", FieldType.Text);
            this.AddField("SortableTitle", "Title", FieldType.Keyword);
            this.AddField("Excerpt", FieldType.Text);
            this.AddField("Summary", FieldType.UnStored);
            this.AddField("Body", FieldType.TextUnStored);
            this.AddField("Path", FieldType.Keyword);
            this.AddField("PublishDate", FieldType.Keyword);
            this.AddField("Status", FieldType.Keyword);

            if (manager["PublishDate"] is DateTime)
            {
                DateTime pubDate = (DateTime)manager["PublishDate"];
                if (pubDate > DateTime.Now)
                {
                    this.Expires = pubDate;
                }
            }

            this.AddField("IsContent", FieldType.Keyword);
            this.AddField("IsContentContainer", FieldType.Keyword);
            this.AddValueField("ParentID", c.ControllerID, FieldType.Keyword);

            this.AddValueField("Text", c.GetIndexableText(), FieldType.TextUnStored);

            this.Created = c.PublishDate;

            if (c.ControllerID != Guid.Empty)
            {
                this.AddValueField("ContainerID", c.ControllerID, FieldType.Keyword);
            }

            this.GroupsWithViewPermissions = c.Permissions.GroupsWithViewPermissions;
        }
    }
}
