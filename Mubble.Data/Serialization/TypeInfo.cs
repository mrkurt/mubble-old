using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.Data.Serialization
{
    public class TypeInfo
    {
        private Type underlyingType;
        public Type Type { get; set; }
        public bool Nullable { get; set; }
        public string Name { get; set; }

        public TypeInfo(Type type)
        {
            this.Type = type;
            this.Name = GetTypeName(type);
        }

        public static string GetTypeName(Type type)
        {
            var tname = type.Name.Replace('+', '.');

            if (type.IsGenericType)
            {
                tname = tname.Substring(0, tname.IndexOf('`'));
                tname += "<";

                var parameters = from t in type.GetGenericArguments()
                                 select GetTypeName(t);

                tname += string.Join(",", parameters.ToArray());

                tname += ">";
            }

            return tname;
        }
    }
}
