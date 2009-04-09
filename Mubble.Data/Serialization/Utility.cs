using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.Data.Serialization
{
    public static class Utility
    {
        public static string Serialize<T>(IData root) where T : ISerializer, new()
        {
            var s = new T();

            s.Start(root.GetType());

            var map = Mubble.Data.Mapping.PropertyMap.GetMap(root.GetType());

            foreach (var key in map.GetKeys())
            {
                var value = root[key];
                var type = new TypeInfo(map.Directors[key].Type);

                s.Append(key, value, type);
            }

            return s.ToString();
        }
    }
}
