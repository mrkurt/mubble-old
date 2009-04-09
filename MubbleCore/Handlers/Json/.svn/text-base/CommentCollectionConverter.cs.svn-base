using System;
using System.Collections.Generic;
using System.Text;
using Mubble.Models;

namespace Mubble.Handlers.Json
{
    class CommentCollectionConverter : Newtonsoft.Json.JsonConverter
    {
        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value)
        {
            CommentCollection c = value as CommentCollection;
            if (c != null)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Page");
                writer.WriteValue(c.PageNumber);
                writer.WritePropertyName("PageCount");
                writer.WriteValue(c.Pages);
                writer.WritePropertyName("PostLink");
                writer.WriteValue(c.PostLink);

                writer.WritePropertyName("Comments");
                base.WriteJson(writer, value);
                writer.WriteEndObject();
            }
        }
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(CommentCollection) || objectType.IsSubclassOf(typeof(CommentCollection)))
            {
                return true;
            }
            return false;
        }
    }
}
