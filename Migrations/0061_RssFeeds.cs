using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class RssFeeds_61 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable t = CreateTable("RssFeeds"))
            {
                t.AddColumn("ID", DataType.UniqueIdentifier, false, "newid()");
                t.PrimaryKey("ID");
                t.AddColumn("Title", DataType.NVarChar(255), false);
                t.AddColumn("Link", DataType.NVarChar(255), false);
                t.AddColumn("Description", DataType.NVarCharMax, false);
                t.AddColumn("ManagingEditor", DataType.NVarChar(255), true);
                t.AddColumn("ItemFormat", DataType.NVarCharMax, true);
                t.AddColumn("Slug", DataType.NVarChar(50), true);
                t.AddColumn("RedirectUrl", DataType.NVarChar(255), true);
                t.AddColumn("RedirectExceptions", DataType.NVarCharMax, true);
            }
            using (ActiveTable t = OpenTable("Controllers"))
            {
                t.HasMany("RssFeeds");
            }
            using (ActiveTable t = OpenTable("RssFeeds"))
            {
                t.Unique("ControllerID", "Slug");
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("RssFeeds");
        }
    }
}
