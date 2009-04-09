using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class DiscussionProviders_53 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable t = CreateTable("DiscussionProviders"))
            {
                t.AddColumn("ID", DataType.UniqueIdentifier, false, "newid()");
                t.AddColumn("Name", DataType.NVarChar(255));
                t.AddColumn("ActiveObjectType", DataType.NVarChar(255), true);
                t.AddColumn("Settings", DataType.NVarCharMax, true);
                t.AddColumn("IsDefault", DataType.Bit, false, "0");
                t.PrimaryKey("ID");
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("DiscussionProviders");
        }
    }
}
