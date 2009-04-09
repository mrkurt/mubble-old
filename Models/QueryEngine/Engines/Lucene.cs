using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Lucene.Net.Index;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Store;
using Lucene.Net.Documents;

namespace Mubble.Models.QueryEngine.Engines
{
    public class Lucene : Engine
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Queue<IndexDocument> IndexQueue = new Queue<IndexDocument>();

        public override string CacheFile
        {
            get { return System.IO.Path.Combine(this.IndexLocation, "Cache.txt"); }
        }

        private string indexLocation;

        public string IndexLocation
        {
            get { return indexLocation; }
            set { indexLocation = value; }
        }

        public static string IdxLocation;
        public static IndexSearcher Searcher;

        static LuceneMetrics metrics = new LuceneMetrics();
        public static LuceneMetrics Metrics { get { return metrics; } }

        public static Lucene Init(string path)
        {
            return Init(path, false);
        }

        static bool ramSearcher = false;

        public static Lucene Init(string path, bool useRamSearcher)
        {
            IdxLocation = path;

            if (!IndexReader.IndexExists(path))
            {
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                IndexWriter idxWriter = new IndexWriter(path, new StandardAnalyzer(), true);
                idxWriter.Close();
                idxWriter = null;
            }
            ramSearcher = useRamSearcher;
            ResetSearcher();
            //Searcher = new IndexSearcher(IdxLocation);
            //if (IdxLocation != null && ramSearcher)
            //{
            //    Searcher = new IndexSearcher(MMapDirectory.GetDirectory(IdxLocation, false));
            //}
            //else if (IdxLocation != null)
            //{
            //    Searcher = new IndexSearcher(IdxLocation);
            //}
            return new Lucene(path);
        }

		static ReaderWriterLock searcherLock = new ReaderWriterLock();
        static DateTime SearcherLastOpened = DateTime.MinValue;

        static void ResetSearcher()
        {
            DateTime requestedAt = DateTime.Now;
            try
            {
                searcherLock.AcquireWriterLock(25000);

				try
				{
                    if (requestedAt < SearcherLastOpened) return;

                    SearcherLastOpened = DateTime.Now;
                    IndexSearcher newSearcher = null;
                    if (IdxLocation != null && ramSearcher)
                    {
                        newSearcher = new IndexSearcher(MMapDirectory.GetDirectory(IdxLocation, false));
                    }
                    else if (IdxLocation != null)
                    {
                        newSearcher = new IndexSearcher(IdxLocation);
                    }
                    metrics.SearcherOpened();

                    log.Info("Opened searcher");

                    IndexSearcher oldSearcher = Searcher;
					Searcher = newSearcher;

					if (oldSearcher != null)
					{
						oldSearcher.Close();
						metrics.SearcherClosed();
						oldSearcher = null;
					}
				}
				finally
				{
					searcherLock.ReleaseWriterLock();
				}
				FireSearcherRefreshed();
			}
			catch (ApplicationException ex)
			{
				log.Error("Timed out waiting for WriterLock on IndexSearcher", ex);
			}
        }

        private bool optimizeOnClose = false;

        private Lucene(string indexLocation)
        {
            this.IndexLocation = indexLocation;
            SetupWriter();
        }

        public override void Optimize()
        {
            this.optimizeOnClose = true;
            //this.EnsureWorkerThread();
        }

        #region Queries
        public override IndexDocument[] Query(Query query)
        {
            DateTime start = DateTime.Now;
            //IndexSearcher searcher = new IndexSearcher(this.IndexLocation);
            global::Lucene.Net.Search.Query q = LuceneQueryTranslator.TranslateQuery(query);
            
            Sort s = null;

            string orderBy = query.OrderBy;
            if (orderBy != null && !orderBy.Equals("Score", StringComparison.CurrentCultureIgnoreCase))
            {
                bool reverse = orderBy.IndexOf(" DESC") > 0;
                orderBy = orderBy.Replace(" DESC", "");
                s = new Sort(orderBy, reverse);
            }
            metrics.SearchesRun++;

			try
			{

				searcherLock.AcquireReaderLock(5000);

				try
				{
					Hits hits = (s != null) ? Searcher.Search(q, s) : Searcher.Search(q);

					List<IndexDocument> results = new List<IndexDocument>();

					int resultCount = query.EndResultIndex;
					query.TotalResults = hits.Length();

					for (int i = query.StartResultIndex; i < hits.Length() && (resultCount < 0 || i < resultCount); i++)
					{
						IndexDocument doc = this.TranslateDocument(hits.Doc(i));
						doc.Score = hits.Score(i);
						doc.IndexID = hits.Id(i);
						results.Add(doc);
					}


					DateTime end = DateTime.Now;

					TimeSpan totalTime = end - start;

					log.DebugFormat("Query took {0:0.000} seconds with {1}", totalTime.TotalSeconds, Searcher.Reader.Directory().GetType().Name);

					return results.ToArray();
				}
				finally
				{
					searcherLock.ReleaseReaderLock();
				}
			}
			catch (ApplicationException ex)
			{
				log.Error("TimedOut waiting for ReaderLock on IndexSearcher", ex);
				throw ex;
			}
        }

        public override DateTime GetDateValue(IndexField field)
        {
            DateTime date = DateTime.MinValue;
            try
            {
                date = DateField.StringToDate(field.Value.ToString());
            }
            catch { }
            return date;
        }

        protected IndexDocument TranslateDocument(Document doc)
        {
            IndexDocument document = new IndexDocument();
            foreach (Field f in doc.Fields())
            {
                FieldType type = FieldType.Keyword;
                if (f.IsIndexed() && f.IsStored() && f.IsTokenized())
                {
                    type = FieldType.Text;
                }
                else if (f.IsIndexed() && f.IsStored() && !f.IsTokenized())
                {
                    type = FieldType.Keyword;
                }
                else if (f.IsIndexed() && !f.IsStored() && f.IsTokenized())
                {
                    type = FieldType.TextUnStored;
                }
                else if (f.IsIndexed() && !f.IsStored() && !f.IsTokenized())
                {
                    type = FieldType.UnStored;
                }
                else if (!f.IsIndexed() && f.IsStored())
                {
                    type = FieldType.UnIndexed;
                }
                document.AddField(type, f.Name(), f.StringValue());       
            }
            return document;
        }
        #endregion

        public override void DeleteDocument(string id)
        {
            Term term = new Term("IndexDocumentID", id);
            Searcher.Reader.DeleteDocuments(term);
        }

        public override string GetIndexingStatus()
        {
            return "blah";
        }

        public override void Abort()
        {
            
        }

        #region Indexing threads

        MiscUtil.Threading.CustomThreadPool threadPool = new MiscUtil.Threading.CustomThreadPool("Indexing threads");

        void SetupWriter()
        {
            threadPool.SetMinMaxThreads(1, 1);
            threadPool.WorkerException += new MiscUtil.Threading.ThreadPoolExceptionHandler(threadPool_WorkerException);
            threadPool.WorkerThreadPriority = ThreadPriority.BelowNormal;
        }

        void threadPool_WorkerException(MiscUtil.Threading.CustomThreadPool pool, MiscUtil.Threading.ThreadPoolWorkItem workItem, Exception e, ref bool handled)
        {
            //log.Error(e.Message);
        }

        public override void IndexDocument(IndexDocument doc)
        {
            IndexQueue.Enqueue(doc);
            if (threadPool.WorkingThreads == 0)
            {
                threadPool.AddWorkItem(new ThreadStart(this.WatchQueue));
            }
        }

        static DateTime LastOptimized = DateTime.MinValue;

        protected void WatchQueue2()
        {
            log.Info("Watching queueueueueue");
        }
        protected void WatchQueue()
        {
            try
            {
                int errorCount = 0;
                int documentsIndexed = 0;				
                while (true)
                {
                    IndexDocument doc = null;
					try
                    {
                        if (IndexQueue.Count > 0)
                        {

                            doc = IndexQueue.Dequeue();

                            IndexReader rdr = IndexReader.Open(IdxLocation);
                            metrics.SearcherOpened();

                            Term term = new Term("IndexDocumentID", doc.ID);
                            rdr.Delete(term);
                            rdr.Close();
                            metrics.SearcherClosed();

							IndexWriter indexWriter = new IndexWriter(IdxLocation, new StandardAnalyzer(), false);
                            indexWriter.AddDocument(this.ConvertToLuceneDocument(doc));

                            DateTime now = DateTime.Now;
                            if(
                                ((now.Hour >= 23 || now.Hour < 6) && 
                                (now - LastOptimized) > TimeSpan.FromHours(12))
                                ||
                                this.optimizeOnClose
                              )
                            {
                                LastOptimized = now;
                                indexWriter.Optimize();
                            }

                            if (this.optimizeOnClose)
                            {
                                this.optimizeOnClose = false;
                                //indexWriter.Optimize();
                            }

							indexWriter.Close();

                            documentsIndexed++;
                        }
                        else
                        {
							ResetSearcher();
							return;
                        }
                    }
                    catch (System.IO.IOException ioex)
                    {
                        log.Error("An IO exception occurred during indexing", ioex);
                        errorCount++;

                        IndexQueue.Enqueue(doc);

                        if (errorCount > 5)
                        {
                            return;
                        }
                        else
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(50));
                        }
                    }
                    catch (Exception e)
                    {
                        if (e is ThreadAbortException)
                        {
                        }
                        else
                        {
                            log.Error("A lucene error occurred", e);
                        }
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is ThreadAbortException)
                {
                    return;
                }
                else
                {
                    log.Error("A lucene error occurred", ex);
                }
            }
        }

        #endregion

        protected Document ConvertToLuceneDocument(IndexDocument doc)
        {
            Document ldoc = new Document();

            this.AddToLuceneDocument(ldoc, FieldType.Keyword, "IndexDocumentID", doc.ID);
            this.AddToLuceneDocument(ldoc, FieldType.Keyword, "IndexDocumentExpires", doc.Expires);

            foreach (IndexField field in doc.Fields)
            {
                this.AddToLuceneDocument(ldoc, field.Type, field.Name, field.Value, field.Boost);
            }

			return ldoc;
        }

        private void AddToLuceneDocument(Document doc, FieldType type, string field, object value)
        {
            this.AddToLuceneDocument(doc, type, field, value, 0f);
        }

        private void AddToLuceneDocument(Document doc, FieldType type, string field, object value, float boost)
        {
            if (value == null) return;
            System.Collections.IEnumerable enumerator = value as System.Collections.IEnumerable;
            if (!(value is string) && enumerator != null)
            {
                foreach (object o in enumerator)
                {
                    this.AddToLuceneDocument(doc, type, field, o, boost);
                }
            }
            else if (value is string || value.GetType().IsValueType)
            {
                if (value is bool && type == FieldType.Keyword)
                {
                    doc.Add(Field.Keyword(field, value.ToString().ToLower()));
                    return;
                }
                else if (value is DateTime && type == FieldType.Keyword)
                {
                    if ((DateTime)value < DateTime.MaxValue)
                    {
                        doc.Add(Field.Keyword(field, (DateTime)value));
                    }
                    return;
                }
                string val = value.ToString();
                Field f = null;
                switch (type)
                {
                    case FieldType.Keyword:
                        f = Field.Keyword(field, val.ToString().ToLower());
                        break;
                    case FieldType.Text:
                        f = Field.Text(field, val);
                        break;
                    case FieldType.TextUnStored:
                        f = Field.Text(field, new System.IO.StringReader(val));
                        break;
                    case FieldType.UnIndexed:
                        f = Field.UnIndexed(field, val);
                        break;
                    case FieldType.UnStored:
                        f = Field.UnStored(field, val.ToString().ToLower());
                        break;
                }

                if (f != null)
                {
                    if (boost > 0) f.SetBoost(boost);
                    doc.Add(f);
                }
            }
        }

    }
}
