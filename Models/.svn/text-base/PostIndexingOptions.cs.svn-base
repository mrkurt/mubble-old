using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using Mubble.Models.QueryEngine;
using System.Text.RegularExpressions;

namespace Mubble.Models
{
    public class PostIndexingOptions : IndexingOptions
    {
        public PostIndexingOptions(DataManager manager)
        {
            Post p = new Post();
            p.DataManager = manager;

            this.UseCustomFields = true;
            this.AddField("Title", FieldType.Text);
            this.AddField("Excerpt", FieldType.Text);
            this.AddValueField("Body", p.BodyShort, FieldType.Text);
            //this.AddField("Body", FieldType.TextUnStored);
            this.AddValueField("Path", p.Url.Path, FieldType.Keyword);
            this.AddValueField("PathExtra", p.Url.Extra, FieldType.Keyword);
            this.AddField("PublishDate", FieldType.Keyword);
            this.AddField("Status", FieldType.Keyword);

            this.AddField("Author", "UserName", FieldType.Keyword);

            if (manager["PublishDate"] is DateTime)
            {
                DateTime pubDate = p.PublishDate;
                if (pubDate > DateTime.Now)
                {
                    this.Expires = pubDate;
                }
            }

            this.Created = p.PublishDate;

            string text = string.Format("{0} {1}", p.Title, Regex.Replace(p.Body, @"\<[^\>]*\>", " "));
            this.AddValueField("Text", text, FieldType.TextUnStored);

            this.AddValueField("IsContent", true, FieldType.Keyword);
            this.AddValueField("IsContentContainer", false, FieldType.Keyword);

            this.AddValueField("ContainerFileName", p.Controller.FileName, FieldType.Keyword);
            this.AddValueField("ContainerID", p.ControllerID, FieldType.Keyword);

            this.GroupsWithViewPermissions = p.Permissions.GroupsWithViewPermissions;
        }
    }
}
