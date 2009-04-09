//using System;
//using Mubble.Indexing;
//using System.Collections.Generic;

//namespace Mubble.Models.QueryEngine.Engines
//{
//    public class Indexer : Engine
//    {
//        static Mubble.Indexing.Schema schema;

//        static Indexer()
//        {
//            schema = new Mubble.Indexing.Schema
//            {
//                DefaultSearchField = "Text",
//                Name = "Mubble",
//                Version = "0.1"
//            };
//        }

//        public static Indexer Get(string path)
//        {
//            return new Indexer(path);
//        }

//        Mubble.Indexing.Index index = null;

//        Indexer(string path)
//        {
//            this.index = Mubble.Indexing.Index.Open(path);
//            this.index.Update(schema);
//        }

//        public override void IndexDocument(IndexDocument document)
//        {
//            Mubble.Indexing.Document doc = Convert(document);
//            doc.Schema = schema;
//            this.index.Update(doc);
//        }

//        Mubble.Indexing.Document Convert(IndexDocument doc)
//        {
//            Mubble.Indexing.Document d = new Mubble.Indexing.Document();

//            foreach (IndexField field in doc.Fields)
//            {
//                d.Fields.Add(
//                    new Mubble.Indexing.Field
//                    {
//                        Name = field.Name,
//                        Values = { field.Value.ToString() }
//                    }
//                    );
//            }
//            d.Schema = schema;
//            return d;
//        }

//        public override void DeleteDocument(string id)
//        {   
//            throw new NotImplementedException();
//        }

//        public override IndexDocument[] Query(Query query)
//        {
//            SearchResults results = this.index.Search(Convert(query));
//            List<IndexDocument> r = new List<IndexDocument>(results.Hits.Count);

//            int resultCount = query.EndResultIndex;
//            query.TotalResults = results.Hits.Count;

//            for (int i = query.StartResultIndex; i < results.Hits.Count && (resultCount < 0 || i < resultCount); i++)
//            {
//                r.Add(Convert(results.Hits[i]));
//            }
//            return r.ToArray();
//        }

//        IndexDocument Convert(Hit hit)
//        {
//            IndexDocument doc = new IndexDocument();

//            doc.Score = hit.Score;
//            foreach (Field f in hit.Fields.All)
//            {
//                doc.AddField(FieldType.Text, f.Name, f.GetValue());
//            }

//            return doc;
//        }

//        Mubble.Indexing.Query Convert(Query query)
//        {
//            Mubble.Indexing.Query q = new Mubble.Indexing.Query();

//            q.Text = query.Text;
            
//            foreach (QueryClause qc in query.Terms)
//            {
//                q.Children.Add(Convert(qc));
//            }

//            return q;
//        }

//        Mubble.Indexing.QueryClause Convert(QueryClause qc)
//        {
//            Mubble.Indexing.QueryClause qc2 = null;
//            if (qc is BooleanClause)
//            {
//                Mubble.Indexing.BooleanClause bc = new Mubble.Indexing.BooleanClause();
//                qc2 = bc;
//                foreach (QueryClause qc3 in ((BooleanClause)qc).Children)
//                {
//                    bc.Children.Add(Convert(qc3));
//                }
//            }else if(qc is TermClause)
//            {
//                TermClause tc = (TermClause)qc;
//                Mubble.Indexing.TermClause tc2 = new Mubble.Indexing.TermClause();

//                tc2.Field = tc.Field;
//                tc2.Value = tc.Value;
//                tc2.ValueType = tc.Type == TermClauseType.Wildcard ? Mubble.Indexing.TermClauseType.Wildcard :
//                    tc.Type == TermClauseType.Fuzzy ? Mubble.Indexing.TermClauseType.Fuzzy : 
//                    Mubble.Indexing.TermClauseType.Term;
//            }
//            if (qc2 == null)
//            {
//                throw new NotImplementedException();
//            }
//            qc2.Boost = qc.Boost;
//            qc2.Type = qc.IsRequired ? QueryClauseType.Required : qc.IsExcluded ? 
//                QueryClauseType.Excluded : QueryClauseType.Normal;

//            return qc2;
//        }

//        public override DateTime GetDateValue(IndexField field)
//        {
//            throw new NotImplementedException();
//        }

//        public override string CacheFile
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public override string GetIndexingStatus()
//        {
//            throw new NotImplementedException();
//        }

//        public override void Optimize()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
