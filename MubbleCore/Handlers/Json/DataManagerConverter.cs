using System;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;

namespace Mubble.Handlers.Json
{
    public class DataManagerConverter : Newtonsoft.Json.JsonConverter
    {
        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value)
        {
            writer.WriteValue(value.GetType().FullName);
        }
        public override bool CanConvert(Type objectType)
        {
            if(objectType.IsSubclassOf(typeof(DataManager)))
            {
                return true;
            }
            return false;
        }
    }
}