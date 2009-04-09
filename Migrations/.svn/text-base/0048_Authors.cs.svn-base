using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class Authors_48 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable t = CreateTable("Authors"))
            {
                t.AddColumn("ID", DataType.UniqueIdentifier, false, "newid()");
                t.AddColumn("UserName", DataType.NVarChar(255));
                t.AddColumn("Email", DataType.NVarChar(255), true);
                t.AddColumn("DisplayName", DataType.NVarChar(255));
                t.AddColumn("Bio", DataType.NVarCharMax, true);

                t.PrimaryKey("ID");
                t.Unique("UserName");
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("Authors");
        }
    }
}
