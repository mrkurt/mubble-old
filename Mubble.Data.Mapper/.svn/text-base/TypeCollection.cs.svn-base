using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace Mubble.Data.Mapper
{
    public class TypeCollection
    {
        public List<DataType> Types { get; set; }

        public TypeCollection()
        {
            this.Types = new List<DataType>();
        }

        public CodeCompileUnit ToCompileUnit()
        {
            var unit = new CodeCompileUnit();

            var types = from t in this.Types
                        where t.Properties.Where(p => !p.IsInherited).Count() > 0
                        select t;

            foreach (var t in types)
            {
                var ns = new CodeNamespace(t.Namespace);
                unit.Namespaces.Add(ns);
                ns.Types.Add(t.ToClass());
                var i = t.ToInterface();
                if(i != null) ns.Types.Add(i);
            }

            return unit;
        }
    }

    public class DataType
    {
        public string Name { get; set; }
        public string Namespace { get; set; }

        public List<Property> Properties { get; set; }

        public DataType()
        {
            this.Properties = new List<Property>();
        }

        public CodeTypeDeclaration ToClass()
        {
            var cs = new CodeTypeDeclaration(this.Name);
            var iname = "I" + cs.Name;
            if (cs.Name.Contains('`'))
            {
                cs.Name = cs.Name.Substring(0, cs.Name.Length - 2);

                if (cs.Name.EndsWith("Base"))
                {
                    iname = "I" + cs.Name.Substring(0, cs.Name.Length - 4);
                }
                cs.Name += "<T>";
            }
            if (iname != null)
            {
                cs.BaseTypes.Add(new CodeTypeReference(iname));
            }

            cs.IsPartial = true;

            foreach (var p in Properties)
            {
                if (p.IsInherited) continue;
                var prop = new CodeMemberProperty();
                prop.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                prop.Name = p.Name;
                prop.Type = new CodeTypeReference(p.Type);

                prop.HasGet = true;
                prop.HasSet = false;

                var indexer = new CodeIndexerExpression(new CodeThisReferenceExpression(), new CodePrimitiveExpression(p.Name));

                var cast = new CodeCastExpression(prop.Type, indexer);

                prop.GetStatements.Add(new CodeMethodReturnStatement(cast));

                cs.Members.Add(prop);
            }

            return cs;
        }

        public CodeTypeDeclaration ToInterface()
        {
            var cs = new CodeTypeDeclaration("I" + this.Name);
            if (cs.Name.Contains('`'))
            {
                cs.Name = cs.Name.Substring(0, cs.Name.Length - 2);

                if (cs.Name.EndsWith("Base"))
                {
                    cs.Name = cs.Name.Substring(0, cs.Name.Length - 4);
                }
            }
            cs.IsPartial = true;
            cs.IsInterface = true;

            foreach (var p in Properties)
            {
                if (p.IsInherited) continue;
                var prop = new CodeMemberProperty();
                prop.Name = p.Name;
                prop.Type = new CodeTypeReference(p.Type);
                prop.HasGet = true;
                prop.HasSet = false;
                cs.Members.Add(prop);
            }
            return cs;
        }
    }

    public class Property
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsInherited { get; set; }
    }
}
