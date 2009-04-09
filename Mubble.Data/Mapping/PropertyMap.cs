using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mubble.Data.Raw;
using System.Threading;
using System.Xml.Linq;
using System.Reflection;

namespace Mubble.Data.Mapping
{
    public class PropertyMap
    {
        // Property -> Property
        // Property -> Through -> Property

        public Type For { get; set; }

        public Dictionary<string, Director> Directors { get; private set; }
        //public Dictionary<string, IProperty> Properties { get; private set; }

        public PropertyMap()
        {
            this.Directors = new Dictionary<string, Director>(StringComparer.CurrentCultureIgnoreCase);
            //this.Properties = new Dictionary<string, IProperty>(StringComparer.CurrentCultureIgnoreCase);
        }

        public object GetValue<T>(string name, T obj, IRecord root)
        {
            return GetValue(name, root);
        }

        public object GetValue(string name, IRecord root)
        {
            var director = this.Directors[name];

            if (string.IsNullOrEmpty(director.Through))
            {
                return root[director.Property];
            }
            else
            {
                root = (IRecord)root[director.Through];
                return root[director.Property];
            }
        }

#if DEBUG
        public XElement Materialize()
        {
            var xml = new XElement("type");
            xml.Add(new XAttribute("name", this.For.Name));
            xml.Add(new XAttribute("namespace", this.For.Namespace));

            foreach (var prop in this.Directors.Keys)
            {
                var d = this.Directors[prop];
                var e = new XElement(
                            "property",
                            new XAttribute("name", prop),
                            new XAttribute("type", d.Type.FullName),
                            new XAttribute("through", d.Through != null ? d.Through : ""),
                            new XAttribute("target", d.Property),
                            new XAttribute("inherited", d.IsInherited)
                            );

                xml.Add(e);
            }
            return xml;
        }

        public static void Save(string path)
        {
            var types = from t in Assembly.GetExecutingAssembly().GetTypes()
                        where typeof(IData).IsAssignableFrom(t) && t.IsClass
                        select t;

            var xml = new XElement("types");

            foreach(var t in types)
            {
                if (!t.IsAbstract && !t.IsGenericTypeDefinition)
                {
                    var obj = Activator.CreateInstance(t);
                }
            }
            foreach(var t in types)
            {
                var map = PropertyMap.GetMap(t);
                if (map != null)
                {
                    xml.Add(map.Materialize());
                }
            }

            xml.Save(path);
        }
#endif

        public List<string> GetKeys()
        {
            return new List<string>(this.Directors.Keys);
        }

        public static PropertyMap Convert<T>(Composer composer)
        {
            return Convert(composer, typeof(T));
        }

        public static PropertyMap Convert(Composer composer, Type type)
        {
            var map = new PropertyMap
            {
                For = type
            };

            for(int i = 0; i < composer.Properties.Count; i++)
            {
                var alias = composer.Properties[i];
                var isInherited = i < composer.FirstNonExtended;
                var t = alias.Type;

                var props = RecordHelper.GetColumns(t, false);

                foreach (var p in props)
                {
                    var prop = p;
                    if (!alias.IsIgnored(prop.Name))
                    {
                        var key = alias.Rename.ContainsKey(prop.Name) ? alias.Rename[prop.Name] : prop.Name;
                        var through = !string.IsNullOrEmpty(alias.Through) ? alias.Property.Name : null;
                        map.Directors.Add(key, new Director
                        {
                            Property = prop.Name,
                            Through = through,
                            Type = prop.ColumnType,
                            IsInherited = isInherited
                        });
                    }
                }
            }

            maps.SyncUpdate(type, map, mapsLock);

            return map;
        }

        static Dictionary<Type, PropertyMap> maps = new Dictionary<Type, PropertyMap>();
        static ReaderWriterLockSlim mapsLock = new ReaderWriterLockSlim();

        public static PropertyMap GetMap(Type type)
        {
            return maps.SyncGet(type, mapsLock);
        }

        public class Director
        {
            public Type Type { get; set; }
            public string Through { get; set; }
            public string Property { get; set; }
            public bool IsInherited { get; set; }
        }
    }
}
