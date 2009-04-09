using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mubble.Data.Raw;

namespace Mubble.Data.Mapping
{
    public interface ITypedName
    {
        Type Type { get; }
        string Name { get; set; }
    }
    public class TypedName<T> : ITypedName where T : IRecord, new()
    {
        public Type Type { get { return typeof(T); } }
        public string Name { get; set; }
        public TypedName(string name)
        {
            this.Name = name;
            var blah = new T();
        }
    }
    public interface IAlias
    {
        string Through { get; set; }
        ITypedName Property { get; set; }
        List<string> Ignore { get; }
        Dictionary<string, string> Rename { get; }
        bool IsIgnored(string name);
        Type Type { get; }
    }
    public class Alias : IAlias
    {
        public string Through { get; set; }
        public ITypedName Property { get; set; }
        public List<string> Ignore { get; private set; }
        public Dictionary<string, string> Rename { get; private set; }
        public Type Type { get { return this.Property.Type; } }

        public Alias()
        {
            this.Ignore = new List<string>();
            this.Rename = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
        }

        public bool IsIgnored(string name)
        {
            if (this.Ignore == null) return false;
            return this.Ignore.Contains(name, StringComparer.CurrentCultureIgnoreCase);
        }
    }
}
