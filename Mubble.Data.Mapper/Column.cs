using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace Mubble.Data.Mapper
{
    public class Column
    {
        public string Name { get; set; }

        private string member;
        public string Member
        {
            get
            {
                if (string.IsNullOrEmpty(member))
                {
                    return this.Name;
                }
                else
                {
                    return this.member;
                }
            }
            set
            {
                this.member = value;
            }
        }

        public bool CanBeNull { get; set; }

        public string Type { get; set; }

        public bool IsPrimaryKey { get; set; }

        public CodeMemberProperty ToProperty()
        {
            CodeMemberProperty prop = new CodeMemberProperty();
            prop.Name = this.Member;

            //var provider = new Microsoft.CSharp.CSharpCodeProvider();

            return null;
            
        }

        public Type GetColumnType()
        {
            var type = System.Type.GetType(this.Type);
            if (this.CanBeNull && type.IsValueType)
            {
                type = typeof(Nullable<>).MakeGenericType(type);
            }
            return type;
        }
    }
}
