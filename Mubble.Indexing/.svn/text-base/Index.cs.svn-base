using System;
using System.Collections.Generic;

using System.Threading;


namespace Mubble.Indexing
{
    public delegate void IndexChanged();
    public partial class Index : IDisposable
    {
        // Need to open a shared searcher on the index
        //  - This needs to be reopened after an indexing op
        //  - Should never be used for writes (like deletes)
        //  - It's ok to use it when it's stale
        // Need to actually search
        // Need to queue up and handle writes (then reset the searcher)
        // Need to handle optimization

        // How should Schemas fit in with these ops?
        //  - Schemaless queries would be nice
        //  - Documents for indexing need to match their assigned schema
        //  - Schemas need to account for undefined fields

        public event IndexChanged Changed;

        void FireChanged()
        {
            if (Changed != null)
            {
                Changed();
            }
        }

        private readonly string path;

        public string Path { get { return this.path; } }

        private readonly Searcher searcher;
        private readonly Writer writer;
        private readonly SchemaManager schemaManager;

        private readonly static ReaderWriterLockSlim indexCollectionLock = new ReaderWriterLockSlim();
        private readonly static Dictionary<string, Index> indexes = 
            new Dictionary<string, Index>(StringComparer.CurrentCultureIgnoreCase);
        
        public static Index Open(string path)
        {
            indexCollectionLock.EnterUpgradeableReadLock();
            try
            {
                if (indexes.ContainsKey(path))
                {
                    return indexes[path];
                }
                else
                {
                    return Create(path);
                }
            }
            finally
            {
                indexCollectionLock.ExitUpgradeableReadLock();
            }
        }

        static Index Create(string path)
        {
            indexCollectionLock.EnterWriteLock();
            try
            {
                if (indexes.ContainsKey(path))
                {
                    return indexes[path];
                }
                else
                {
                    var index = new Index(path);
                    indexes.Add(path, index);
                    return index;
                }
            }
            finally
            {
                indexCollectionLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Opens or creates an index at the specified location
        /// </summary>
        /// <param name="path">The index path</param>
        private Index(string path)
        {
            this.path = path;
            Utility.EnsureIndex(this.path);

            this.searcher = new Searcher(this);
            this.writer = new Writer(this);
            this.writer.Changed += FireChanged;
            this.schemaManager = new SchemaManager(this);
        }

        public IndexWorkItem Update(Document document)
        {
            var wi = new IndexWorkItem();
            Update(document, wi);
            return wi;
        }

        public void Update(Document document, IndexWorkItem workItem)
        {
            //TODO: Validate against the supplied schema
            this.writer.Update(document, workItem);
        }

        public IndexWorkItem Update(IEnumerable<Document> batch)
        {
            var wi = new IndexWorkItem();
            Update(batch, wi);
            return wi;
        }

        public void Update(IEnumerable<Document> batch, IndexWorkItem workItem)
        {
            //TODO: Validate against the supplied schema
            this.writer.Update(batch, workItem);
        }


        public void Update(Schema schema)
        {
            this.schemaManager.Save(schema);
        }

        public Schema GetSchema(string name, string version)
        {
            return this.schemaManager.GetSchema(name, version);
        }

        public SearchResults Search(string query)
        {
            return this.searcher.Search(new Query { Text = query });
        }

        public SearchResults Search(Query query)
        {
            return this.searcher.Search(query);
        }

        #region IDisposable Members

        public void Dispose()
        {
            //TODO: Figure out how to clean all this crap up
            this.searcher.Dispose();
            this.writer.Dispose();
        }

        #endregion
    }
}
