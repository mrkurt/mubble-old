using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace Mubble.Data.Mapper
{
    public class Database
    {
        public string Name { get; set; }
        public string EntityNamespace { get; set; }

        public List<Table> Tables { get; set; }

        public CodeCompileUnit ToCompileUnit()
        {
            var unit = new CodeCompileUnit();
            var ns = new CodeNamespace(this.EntityNamespace);
            unit.Namespaces.Add(ns);

            foreach (var t in this.Tables)
            {
                ns.Types.Add(t.ToClass());
            }

            return unit;
        }
    }
}
