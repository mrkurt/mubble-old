using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mubble.Data.Mapper
{
    public static class DbmlHelper
    {
        public static Database Convert(string path)
        {
            var doc = XDocument.Load(path);

            return Convert(doc);
        }

        static XNamespace ns = "http://schemas.microsoft.com/linqtosql/dbml/2007";
        public static Database Convert(XDocument doc)
        {            
            var tables = from node in doc.Descendants(ns + "Database")
                         select new Database
                         {
                             Name = node.Attribute("Name").Value,
                             EntityNamespace = node.Attribute("EntityNamespace").Value,
                             Tables = (from t in node.Descendants(ns + "Table")
                                       select new Table 
                                                { 
                                                    Name = t.Attribute("Member").Value,
                                                    Columns = GetColumns(t),
                                                    Associations = GetAssociations(t)
                                                }).ToList()
                         };

            return tables.FirstOrDefault();
        }

        static List<Column> GetColumns(XElement node)
        {
            var cols = from col in node.Descendants(ns + "Column")
                       select new Column
                       {
                           Name = col.Attribute("Name").Value,
                           Type = col.Attribute("Type").Value,
                           Member = col.IfAttribute("Member"),
                           CanBeNull = col.IfAttribute("CanBeNull", false),
                           IsPrimaryKey = col.IfAttribute("IsPrimaryKey", false)
                       };

            return cols.ToList();
        }

        static List<Association> GetAssociations(XElement node)
        {
            var associations = from a in node.Descendants(ns + "Association")
                               select new Association
                               {
                                   Member = a.IfAttribute("Member"),
                                   ThisKey = a.IfAttribute("ThisKey"),
                                   OtherKey = a.IfAttribute("OtherKey"),
                                   Type = a.IfAttribute("Type"),
                                   Cardinality = a.IfAttribute("Cardinality"),
                                   IsForeignKey = a.IfAttribute("IsForeignKey", false)
                               };
            return associations.ToList();
        }

        static string IfAttribute(this XElement node, string name)
        {
            var attr = node.Attribute(name);

            if (attr != null)
            {
                return attr.Value;
            }
            else
            {
                return null;
            }
        }

        static bool IfAttribute(this XElement node, string name, bool defaultValue)
        {
            var raw = node.IfAttribute(name);
            if (string.IsNullOrEmpty(raw))
            {
                return defaultValue;
            }
            bool value = defaultValue;

            bool.TryParse(raw, out value);

            return value;
        }
    }
}
