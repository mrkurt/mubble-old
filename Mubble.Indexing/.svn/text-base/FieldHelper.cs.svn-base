using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mubble.Indexing
{
    public delegate string ValueFormatter(SchemaField field, string raw);
    public static class FieldHelper
    {
        static readonly Dictionary<string, ValueFormatter> Formatters = new Dictionary<string, ValueFormatter>();

        static FieldHelper()
        {
            Formatters.Add("datetime", DateFormatter);
        }

        public static string Format(SchemaField field, string raw)
        {
            if (Formatters.ContainsKey(field.Type))
            {
                return Formatters[field.Type](field, raw);
            }
            else
            {
                return RawString(field, raw);
            }
        }

        static string RawString(SchemaField field, string raw)
        {
            return raw;
        }

        static string DateFormatter(SchemaField field, string raw)
        {
            var date = DateTime.Parse(raw);

            return Lucene.Net.Documents.DateTools.DateToString
                (
                date, 
                Lucene.Net.Documents.DateTools.Resolution.MINUTE
                );
        }
    }
}
