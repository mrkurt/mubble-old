using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Mubble.Indexing;

namespace Mubble.Tests
{
    [TestFixture]
    public class DatabaseStore
    {
        Database GetDB()
        {
            return new Database(
                Index.Open("LuceneIndex"),
                new Database.Settings
                {
                    ConnectionString = @"Data Source=localhost\SQLExpress; Database=mubble; Integrated Security=true;"
                }
                );
        }

        [Test]
        public void Create()
        {
            var db = GetDB();
        }

        [Test]
        public void SaveSome()
        {
            var db = GetDB();
            var list = new List<Guid>(100);
            db.Clear();
            for (int i = 0; i < 100; i++)
            {
                var guid = Guid.NewGuid();
                list.Add(guid);
                var doc = Indexing.GetDocument();
                db.Save(doc, guid);
            }

            foreach (var id in list)
            {
                db.Delete(id);
            }
        }

        [Test]
        public void UpdateFrom()
        {
            var db = GetDB();
            Indexing.WaitFor(db.UpdateIndex());
        }
    }
}
