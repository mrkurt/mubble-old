using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.Reflection;

namespace Mubble.Data.Mapper
{
    public class Table
    {
        public string Name { get; set; }
        public List<Column> Columns { get; set; }
        public List<Association> Associations { get; set; }

        public CodeTypeDeclaration ToClass()
        {
            var cs = new CodeTypeDeclaration(this.Name.Substring(0, this.Name.Length - 1));
            cs.IsClass = true;
            cs.IsPartial = true;
            //cs.TypeAttributes = (cs.TypeAttributes & ~TypeAttributes.VisibilityMask) | TypeAttributes.NotPublic;

            var pks = (from c in this.Columns
                      where c.IsPrimaryKey
                      select c).ToArray();

            if (pks.Length == 1 && pks[0].Type == "System.Guid" && pks[0].Member == "ID")
            {
                cs.BaseTypes.Add("IStandardRecord");
            }
            else
            {
                cs.BaseTypes.Add("IRecord");
            }

            cs.Members.Add(RegisterColums());

            var indexer = GetIndexer();

            cs.Members.Add(indexer);

            return cs;
        }


        CodeTypeConstructor RegisterColums()
        {
            var c = new CodeTypeConstructor();
            foreach (var col in this.Columns)
            {
                c.Statements.Add(GetRegisterMethod("RegisterColumn", col.Member, new CodeTypeReference(col.GetColumnType())));
            }

            foreach(var a in this.Associations)
            {
                if (a.IsForeignKey || (!a.IsForeignKey && a.Cardinality == "One"))
                {
                    c.Statements.Add(GetRegisterMethod("RegisterAssociation", a.Member, new CodeTypeReference(a.Type)));
                }
            }

            return c;
        }

        CodeMethodInvokeExpression GetRegisterMethod(string method, string member, CodeTypeReference type)
        {
            var helper = new CodeTypeReferenceExpression("RecordHelper");

            var name = new CodeSnippetExpression("\"" + member + "\"");
            var getter = new CodeSnippetExpression(
                string.Format("obj => obj.{0}", member)
                );
            var setter = new CodeSnippetExpression(
                string.Format("(obj, val) => obj.{0} = val", member)
                );

            var bl = new CodeMethodReferenceExpression(helper, method,
                new CodeTypeReference(this.Name.Substring(0, this.Name.Length - 1)),
                type);
            var g = new CodeMethodInvokeExpression(bl, name, getter, setter);

            return g;
        }

        static CodeMemberProperty GetIndexer()
        {
            var indexer = new CodeMemberProperty();
            indexer.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            indexer.Name = "Item";
            indexer.Type = new CodeTypeReference(typeof(object));
            indexer.HasSet = true;
            indexer.HasGet = true;
            indexer.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "field"));

            //return RecordHelper.Get(this, field);
            //RecordHelper.Set(this, field, value);
            var helper = new CodeTypeReferenceExpression("RecordHelper");
            var field = new CodeVariableReferenceExpression("field");
            var _this = new CodeThisReferenceExpression();
            var value = new CodeVariableReferenceExpression("value");

            var g = new CodeMethodInvokeExpression(helper, "Get", _this, field);
            indexer.GetStatements.Add(new CodeMethodReturnStatement(g));


            var s = new CodeMethodInvokeExpression(helper, "Set", _this, field, value);
            indexer.SetStatements.Add(s);
            return indexer;
        }
    }
}
