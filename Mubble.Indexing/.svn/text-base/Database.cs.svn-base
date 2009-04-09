using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Mubble.Indexing
{
    public class Database
    {
        // Need to start watching a table for changes targeting a particular index
        //  - Document, Status (Deleted or Active), LastModified
        // 
        // Grab newly modified documents, hand them off to specified index
        // Store max modified date

        //SqlDependency dep = null;

        private readonly Index index;
        private readonly Settings settings;

        public Database(string indexPath, Settings settings) : this(Index.Open(indexPath), settings) { }

        public Database(Index index, Settings settings)
        {
            this.index = index;
            this.settings = settings;
        }

        public void Save(Document doc)
        {
            Save(doc, null);
        }

        public void Save(Document doc, object id)
        {
            Save(doc, id, RowStatus.Active);
        }

        void Save(Document doc, object id, RowStatus status)
        {
            var sql = this.settings.GetUpdateSql();

            using (var cn = new SqlConnection(this.settings.ConnectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Document", doc != null ? (object)Utility.Serialize(doc) : DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", status);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public void Delete(object id)
        {
            Save(null, id, RowStatus.Deleted);
        }

        public void Clear()
        {
            var sql = "DELETE FROM " + this.settings.Table;
            using (var cn = new SqlConnection(this.settings.ConnectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public IndexWorkItem UpdateIndex()
        {
            var last = GetLastRecordDate();
            var docs = new List<Document>();

            using(var cn = new SqlConnection(this.settings.ConnectionString))
            using (var cmd = new SqlCommand(this.settings.GetSql(), cn))
            {
                cmd.Parameters.AddWithValue("@LastTime", last);

                cn.Open();
                var rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read())
                {
                    var xml = rdr[this.settings.DocumentColumn] as string;
                    var status = (RowStatus)rdr[this.settings.StatusColumn];

                    if (status != RowStatus.Deleted && !string.IsNullOrEmpty(xml))
                    {
                        docs.Add(Utility.Deserialize<Document>(xml));
                    }
                }
            }

            return this.index.Update(docs);
        }

        DateTime GetLastRecordDate()
        {
            var path = index.IndexTimestamp();
            var date = new DateTime(1753, 1, 1);

            if (!File.Exists(path))
            {
                return date;
            }

            var raw = File.ReadAllText(path);

            if (DateTime.TryParse(raw, out date))
            {
                return date;
            }
            return date;
        }

        public enum RowStatus
        {
            Active = 1,
            Deleted = 0
        }

        public class Settings
        {
            public string ConnectionString { get; set; }
            public string Table { get; set; }
            public string PrimaryKey { get; set; }
            public string DocumentColumn { get; set; }
            public string TimestampColumn { get; set; }
            public string StatusColumn { get; set; }

            public Settings()
            {
                this.Table = "dbo.IndexItems";
                this.PrimaryKey = "ContentItemID";
                this.DocumentColumn = "Document";
                this.TimestampColumn = "LastUpdated";
                this.StatusColumn = "Status";
            }

            internal string GetWatchSql()
            {
                return string.Format(
                    "SELECT {0} FROM {1}", 
                    this.TimestampColumn, 
                    this.Table
                    );
            }

            internal string GetSql()
            {
                return string.Format(
                    @"SELECT {0} as Document, {1} as Status, {2} as Timestamp FROM {3} WHERE {2} > @LastTime
                    ORDER BY {2}", 
                    this.DocumentColumn, 
                    this.StatusColumn, 
                    this.TimestampColumn, 
                    this.Table
                    );
            }

            internal string GetUpdateSql()
            {
                var sql = @"
                    IF @Status = 0
                        BEGIN
                            UPDATE {4} SET {3} = @Status WHERE {0} = @ID
                            RETURN
                        END
                    IF @ID IS NULL
                        BEGIN
                            INSERT INTO {4} ({1}, {2}, {3}) VALUES( @Document, getdate(), @Status )
                            RETURN
                        END
                    
                    IF NOT EXISTS( SELECT {0} FROM {4} WHERE {0} = @ID )
                        BEGIN
                            INSERT INTO {4} ({0}, {1}, {2}, {3}) VALUES( @ID, @Document, getdate(), @Status )
                            RETURN
                        END
                    UPDATE {4} SET {1} = @Document, {2} = getdate(), {3} = @Status WHERE {0} = @ID
                ";

                return string.Format(
                    sql,
                    this.PrimaryKey,
                    this.DocumentColumn,
                    this.TimestampColumn,
                    this.StatusColumn,
                    this.Table
                    );
            }
        }
    }
}
