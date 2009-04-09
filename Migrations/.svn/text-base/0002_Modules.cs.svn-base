using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class Modules_2 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            DeleteTableIfExists("Modules");
            using (ActiveTable modules = CreateTable("Modules"))
            {
                modules.AddColumn("ID", DataType.UniqueIdentifier, false, "newid()");
                modules.AddColumn("Name", DataType.NVarChar(255));
                modules.AddColumn("Path", DataType.NVarChar(255));
                modules.AddColumn("UpdatedAt", DataType.DateTime, true, "getdate()");
                modules.PrimaryKey("ID");
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("Modules");
        }
    }
}
