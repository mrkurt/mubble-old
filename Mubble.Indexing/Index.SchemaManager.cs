using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Mubble.Indexing
{
	public partial class Index
	{
        class SchemaManager
        {
            private readonly Index index;
            private readonly ReaderWriterLockSlim schemaLock;
            private readonly Dictionary<string, Schema> cache;
            
            public SchemaManager(Index index)
            {
                this.index = index;
                this.schemaLock = new ReaderWriterLockSlim();
                this.cache = new Dictionary<string, Schema>(StringComparer.CurrentCultureIgnoreCase);
            }

            public Schema GetSchema(string name, string version)
            {
                string key = string.Concat(name, "[", version, "]");
                Schema schema = null;
                schemaLock.EnterUpgradeableReadLock();
                try
                {
                    if (cache.ContainsKey(key))
                    {
                        schema = cache[key];
                    }
                    else
                    {
                        schemaLock.EnterWriteLock();
                        try
                        {
                            if (cache.ContainsKey(key))
                            {
                                schema = cache[key];
                            }
                            else
                            {
                                using (var file = File.OpenRead(IndexHelper.SchemaPath(this.index, name, version)))
                                {
                                    schema = Utility.Deserialize<Schema>(file);
                                }
                                cache.Add(key, schema);
                            }
                        }
                        finally
                        {
                            schemaLock.ExitWriteLock();
                        }
                    }
                }
                finally
                {
                    schemaLock.ExitUpgradeableReadLock();
                }
                return schema;
            }

            public void Save(Schema schema)
            {
                using (var file = File.Create(IndexHelper.SchemaPath(this.index, schema)))
                {
                    Utility.Serialize(schema, file);
                }
            }
        }
	}
}
