using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Mubble.Data;

namespace Mubble.Web.Output
{
    public class Json : ISerializer
    {
        JsonTextWriter json;
        StringBuilder output = new StringBuilder();
        JsonSerializer serializer;
        Stack<ClosingAction> closings = new Stack<ClosingAction>();

        delegate void ClosingAction();

        public Json()
        {
            var writer = new System.IO.StringWriter(output);
            json = new JsonTextWriter(writer);
            json.Formatting = Formatting.Indented;
            serializer = new JsonSerializer();
        }

        public static string Serialize(IData data)
        {
            return JavaScriptConvert.SerializeObject(data);
        }

        public override string ToString()
        {
            CloseAll();
            
            return output.ToString();
        }

        void CloseAll()
        {
            while (closings.Count > 0)
            {
                closings.Pop()();
            }
        }

        #region ISerializer Members

        public void Start(Mubble.Data.IData data, TypeInfo type)
        {
            json.WriteStartObject();
            //json.WritePropertyName("type");

            //json.WriteValue(type.Name);

            closings.Push(json.WriteEndObject);
        }

        public void AppendField(string name, object data, TypeInfo type)
        {
            //throw new NotImplementedException();
            json.WritePropertyName(name);
            var val = data is Guid  ? data.ToString()
                                    : JavaScriptConvert.ToString(data);
            serializer.Serialize(json, data);
        }

        public void StartRelated(string name, IData data, TypeInfo type)
        {
            json.WritePropertyName(name);
            json.WriteStartObject();
            closings.Push(json.WriteEndObject);
        }

        public void EndBlock()
        {
            closings.Pop()();
        }

        #endregion
    }
}
