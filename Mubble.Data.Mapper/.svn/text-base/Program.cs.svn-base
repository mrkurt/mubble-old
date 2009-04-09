using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.CodeDom;
using System.Reflection;

namespace Mubble.Data.Mapper
{
    static class Program
    {
        [STAThread]
        static void Main(params string[] args)
        {
            var path = @"..\..\..\Mubble.Data\Raw\Database.dbml";
            var dataTypeAssemblyPath = @"..\..\..\Mubble.Data\bin\Debug\Mubble.Data.dll";
            var dataTypePath = @"..\..\..\Mubble.Data\Generated\DataTypes.xml";
            var output = @"..\..\..\Mubble.Data\Generated\RecordTypes.cs";
            var dataTypeOutput = @"..\..\..\Mubble.Data\Generated\DataTypes.cs";

            LoadDataTypeAssembly(dataTypeAssemblyPath, dataTypePath);

            var cs = new Microsoft.CSharp.CSharpCodeProvider();

            var types = DataObjectHelper.Convert(dataTypePath);

            var c = Generate(types.ToCompileUnit(), cs);

            File.WriteAllText(dataTypeOutput, c);

            var database = DbmlHelper.Convert(path);

            var code = Generate(database.ToCompileUnit(), cs);

            File.WriteAllText(output, code);

            //Console.ReadLine();
        }

        static string Generate(CodeCompileUnit c, System.CodeDom.Compiler.CodeDomProvider provider)
        {
            using (var wrtr = new StringWriter())
            {
                provider.GenerateCodeFromCompileUnit(c, wrtr, new System.CodeDom.Compiler.CodeGeneratorOptions());

                return wrtr.ToString();
            }
        }

        static void LoadDataTypeAssembly(string path, string output)
        {
            path = System.IO.Path.Combine(Environment.CurrentDirectory, path);
            var assembly = Assembly.LoadFile(path);
            var type = assembly.GetType("Mubble.Data.Mapping.PropertyMap");
            var method = type.GetMethod("Save", BindingFlags.Static | BindingFlags.Public);
            method.Invoke(null, new object[]{ output });
        }
    }
}
