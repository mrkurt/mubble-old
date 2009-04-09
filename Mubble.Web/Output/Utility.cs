using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mubble.Data;

namespace Mubble.Web.Output
{
    public static class Utility
    {
        //public static string Render<T>(Response.ISingleItem data)
        //    where T : ISerializer, new()
        //{
        //    var s = new T();
        //}

        public static string Render<T>(IData data)
            where T : ISerializer, new()
        {
            var s = new T();
            s.Start(data, new TypeInfo(data.GetType()));

            var map = Mubble.Data.Mapping.PropertyMap.GetMap(data.GetType());

            foreach (var key in map.GetKeys())
            {
                var value = data[key];
                var type = new TypeInfo(map.Directors[key].Type);

                s.AppendField(key, value, type);
            }

            s.StartRelated("owner", null, new TypeInfo(typeof(Author)));

            s.AppendField("id", Guid.NewGuid(), new TypeInfo(typeof(Guid)));
            s.AppendField("email", "kurt@mubble.net", new TypeInfo(typeof(string)));
            s.EndBlock();

            return s.ToString();
        }
    }
}
