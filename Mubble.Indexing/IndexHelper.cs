using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using LDocument = Lucene.Net.Documents.Document;
using LField = Lucene.Net.Documents.Field;

using Lucene.Net.Index;
using Lucene.Net.Analysis.Standard;

namespace Mubble.Indexing
{
    internal static class IndexHelper
    {
        public readonly static string SchemaNameField = "Index.Schema.Name";
        public readonly static string SchemaVersionField = "Index.Schema.Version";

        internal static void IndexDocuments(string path, List<IndexOperation> batch)
        {
            var uniques = new Dictionary<string, Document>();

            var deletes = new List<Term>();
            var writes = new List<LDocument>();

            for (var i = batch.Count - 1; i >= 0; i--)
            {
                var op = batch[i];
                var doc = batch[i].Document;
                var schema = doc.Schema;
                var uniqueFields = doc.GetUniqueFields(schema);
                bool indexDoc = op.Type == IndexOperationType.Save;
                foreach (var field in uniqueFields)
                {
                    var sig = field.GetSignature(schema);
                    if (uniques.ContainsKey(sig))
                    {
                        indexDoc = false;
                        break;
                    }
                }
                if (indexDoc)
                {
                    foreach (var field in uniqueFields)
                    {
                        uniques.Add(field.GetSignature(schema), doc);
                        var t = new Term(field.Name, field.GetValue());
                        deletes.Add(t);
                    }
                    writes.Add(Convert(doc, schema));
                }
            }

            var wrtr = new IndexWriter(path, new StandardAnalyzer());
            wrtr.DeleteDocuments(deletes.ToArray());
            wrtr.AddDocuments(writes.ToArray());
            if (deletes.Count + writes.Count > 99)
            {
                wrtr.Optimize();
            }
            wrtr.Close();
        }

        public static void AddDocuments(this IndexWriter wrtr, LDocument[] docs)
        {
            foreach (var d in docs)
            {
                wrtr.AddDocument(d);
            }
        }

        public static LDocument Convert(Document doc, Schema schema)
        {
            var ldoc = new LDocument();
            foreach (var sf in schema.Fields)
            {
                foreach (var lf in Convert(sf, doc))
                {
                    ldoc.Add(lf);
                }
            }
            ldoc.Add(
                new LField(
                    SchemaNameField,
                    schema.Name,
                    ConvertToStore(true, false),
                    ConvertToIndexFlag(false, false)
                    )
                    );

            ldoc.Add(
                new LField(
                    SchemaVersionField,
                    schema.Version,
                    ConvertToStore(true, false),
                    ConvertToIndexFlag(false, false)
                    )
                    );
            return ldoc;
        }

        public static IEnumerable<LField> Convert(SchemaField field, Document doc)
        {
            var f = doc.Fields[field.Name];
            if (f == null) yield break;

            var values = field.MultiValued ? f.Values.ToArray() : new string[]{ f.GetValue() };

            foreach (var value in values)
            {
                yield return new LField(
                    field.Name,
                    FieldHelper.Format(field, value),
                    ConvertToStore(field.Stored, field.Compressed),
                    ConvertToIndexFlag(field.Indexed, field.Tokenized)
                    );

            }
        }

        public static Lucene.Net.Documents.Field.Index ConvertToIndexFlag(bool index, bool tokenize)
        {
            if (index && tokenize)
            {
                return Lucene.Net.Documents.Field.Index.TOKENIZED;
            }
            else if (index)
            {
                return Lucene.Net.Documents.Field.Index.UN_TOKENIZED;
            }
            else
            {
                return Lucene.Net.Documents.Field.Index.NO;
            }
        }

        public static Lucene.Net.Documents.Field.Store ConvertToStore(bool store, bool compress)
        {
            if(store && compress)
            {
                return Lucene.Net.Documents.Field.Store.COMPRESS;
            }
            else if (store)
            {
                return Lucene.Net.Documents.Field.Store.YES;
            }
            else
            {
                return Lucene.Net.Documents.Field.Store.NO;
            }
        }

        public static string SchemaPath(this Index index, Schema schema)
        {
            return SchemaPath(index, schema.Name, schema.Version);
        }

        public static string SchemaPath(this Index index, string name, string version)
        {
            var root = Path.Combine(index.Path, "Schemas");
            root = Path.Combine(root, name);

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            return Path.Combine(root, version + ".xml");
        }

        public static string IndexTimestamp(this Index index)
        {
            return Path.Combine(index.Path, "LastRecordTime.txt");
        }

        public static void SetIndex(this IEnumerable<Document> batch, Index index)
        {
            foreach (var doc in batch)
            {
                doc.Index = index;
            }
        }

        public static void SetIndex(this IEnumerable<IndexOperation> batch, Index index)
        {
            foreach (var doc in batch)
            {
                doc.Document.Index = index;
            }
        }
    }
}
