using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using Lucene.Net.Index;
using Lucene.Net.Analysis.Standard;

namespace Mubble.Indexing
{
    public static class Utility
    {
        public static string Serialize<T>(T obj)
        {
            using (StringWriter str = new StringWriter())
            {
                Serialize(obj, str);
                return str.ToString();
            }
        }

        public static void Serialize<T>(T obj, TextWriter target)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var s = new XmlSerializer(typeof(T));
            s.Serialize(target, obj, ns);
        }

        public static void Serialize<T>(T obj, Stream target)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var s = new XmlSerializer(typeof(T));
            s.Serialize(target, obj, ns);
        }

        public static T Deserialize<T>(string xml)
        {
            using (var txt = new StringReader(xml))
            {
                return Deserialize<T>(txt);
            }
        }

        public static T Deserialize<T>(TextReader rdr)
        {
            var s = new XmlSerializer(typeof(T));
            return (T)s.Deserialize(rdr);
        }

        public static T Deserialize<T>(Stream source)
        {
            var s = new XmlSerializer(typeof(T));
            return (T)s.Deserialize(source);
        }

        internal static void EnsureIndex(string path)
        {
            if (!IndexReader.IndexExists(path))
            {
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                IndexWriter idxWriter = new IndexWriter(path, new StandardAnalyzer(), true);
                idxWriter.Close();
            }
        }

        public static string Join(this IEnumerable<string> values, string divider)
        {
            var s = "";

            foreach (var v in values)
            {
                if (s.Length > 0) s += divider;
                s += v;
            }

            return s;
        }
    }
}
