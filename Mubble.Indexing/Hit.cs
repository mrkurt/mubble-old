using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Search;

namespace Mubble.Indexing
{
    public class Hit
    {
        public float Score { get; set; }

        public string SchemaName { get; set; }
        public string SchemaVersion { get; set; }

        public FieldCollection Fields { get; set; }

        internal static Hit FromRaw(Hits raw, int index)
        {
            var doc = raw.Doc(index);
            var fields = new FieldCollection();

            foreach (var f in doc.GetFields())
            {
                var rf = (Lucene.Net.Documents.Field)f;
                fields.Add(new Field
                {
                    Name = rf.Name(),
                    Values = { rf.StringValue() }
                });
            }

            return new Hit
            {
                Score = raw.Score(index),
                SchemaName = doc.GetValues(IndexHelper.SchemaNameField).Join(""),
                SchemaVersion = doc.GetValues(IndexHelper.SchemaVersionField).Join(""),
                Fields = fields
            };
        }
    }
}
