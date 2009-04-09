using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Mubble.Indexing;
using System.IO;
using MbUnit.Framework;

namespace Mubble.Tests
{
    [TestFixture]
    public class Indexing
    {
        [Test]
        public void Store()
        {
            var index = Index.Open("LuceneIndex");

            Assert.IsTrue(Directory.Exists("LuceneIndex"));
        }

        [Test]
        public void Schema()
        {
            var schema = GetSchema();
            var index = Index.Open("LuceneIndex");

            index.Update(schema);
        }

        public static Document GetDocument()
        {
            var doc = new Document();
            doc.Fields.Add(new Field { Name = "ID", Values = { "12345" } });
            doc.Fields.Add(new Field { Name = "Title", Values = { "Kurt Rocks" } });
            doc.Fields.Add(new Field { Name = "Text", Values = { "Lorem Ipsum Kurtum Spurtum Kurt rules" } });
            doc.Fields.Add(new Field { Name = "Tag", Values = { "Kurt", "Owns", "Joo" } });

            doc.Schema = GetSchema();

            return doc;
        }

        [Test]
        public void IndexDocument()
        {
            var doc = GetDocument();
            var index = Index.Open("LuceneIndex");
            var wi = index.Update(doc);
            WaitFor(wi);
        }

        [Test]
        public void AdHocSchema()
        {
            var doc = GetDocument();
            var schema = new Mubble.Indexing.Schema("mubble", "1.1");
            schema.Fields.Add(new Mubble.Indexing.SchemaField { Name = "ID", Unique = true, Tokenized = false, Stored = true });
            schema.Fields.Add(new Mubble.Indexing.SchemaField { Name = "Title", Stored = true });
            schema.Fields.Add(new Mubble.Indexing.SchemaField { Name = "Text", Stored = true });
            schema.Fields.Add(new Mubble.Indexing.SchemaField { Name = "Tag", Stored = true, MultiValued = true, Tokenized = false });

            doc.Schema = schema;

            var index = Index.Open("LuceneIndex");

            var wi = index.Update(doc);
            WaitFor(wi);

            Assert.IsFalse(IndexWorkItemStatus.Error == wi.Status, "Error indexing document");
        }

        [Test]
        public void IndexABunch()
        {
            var index = Index.Open("LuceneIndex");

            var workItems = new List<IndexWorkItem>();

            for (var i = 0; i < 100; i++)
            {
                var doc = GetDocument();
                doc.Fields.Replace(new Field { Name = "ID", Values = { i.ToString() } });
                workItems.Add(index.Update(doc));
            }
            WaitFor(workItems);
        }

        [Test]
        public void IndexBatch()
        {
            var docs = new List<Document>();

            for (int i = 0; i < 100; i++)
            {
                var doc = GetDocument();
                doc.Fields.Replace(new Field { Name = "ID", Values = { i.ToString() } });
                docs.Add(doc);
            }

            var index = Index.Open("LuceneIndex");
            var wi = index.Update(docs);

            WaitFor(wi);
        }

        [Test]
        public void Search()
        {
            var index = Index.Open("LuceneIndex");

            var results = index.Search("kurt");

            Console.WriteLine(results.Hits.Count);
        }

        [Test]
        public void SerializeDocument()
        {
            var doc = GetDocument();

            using (var file = File.Create("document.xml"))
            {
                Utility.Serialize(doc, file);
            }
        }

        public static void WaitFor(IndexWorkItem workItem)
        {
            workItem.WaitOne();
        }

        public static void WaitFor(IList<IndexWorkItem> workItems)
        {
            for (int i = 0; i < workItems.Count; i++)
            {
                var wi = workItems[i];
                wi.WaitOne();
            }
        }

        static Schema GetSchema()
        {
            var schema = new Mubble.Indexing.Schema("mubble", "1.0");
            schema.Fields.Add(new Mubble.Indexing.SchemaField { Name = "ID", Unique = true, Tokenized = false, Stored = true });
            schema.Fields.Add(new Mubble.Indexing.SchemaField { Name = "Title", Stored = true });
            schema.Fields.Add(new Mubble.Indexing.SchemaField { Name = "Text", Stored = true });
            schema.Fields.Add(new Mubble.Indexing.SchemaField { Name = "Tag", Stored = true, MultiValued = true, Tokenized = false });

            return schema;
        }
    }
}
