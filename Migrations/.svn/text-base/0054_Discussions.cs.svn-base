using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class Discussions_54 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            DeleteTableIfExists("Discussions");
            using (ActiveTable t = CreateTable("Discussions"))
            {
                t.AddColumn("ID", DataType.UniqueIdentifier, false, "newid()");
                t.AddColumn("ActiveObjectType", DataType.NVarChar(255));
                t.AddColumn("ActiveObjectID", DataType.UniqueIdentifier);
                t.AddColumn("DiscussionLink", DataType.NVarChar(255), true);
                t.AddColumn("Status", DataType.Int, false, "0", ActiveColumnAccessMode.Custom);
                t.AddColumn("LastStatusMessage", DataType.NVarChar(255), true);
                t.PrimaryKey("ID");
                t.Unique("ActiveObjectType", "ActiveObjectID");
            }
            using (ActiveTable t = OpenTable("DiscussionProviders"))
            {
                t.HasMany("Discussions");
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("Discussions");
        }
    }
}
