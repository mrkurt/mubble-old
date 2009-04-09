using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class Templates_5 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            DeleteTableIfExists("Templates");
            using (ActiveTable templates = CreateTable("Templates"))
            {
                templates.AddColumn("ID", DataType.UniqueIdentifier, false, "newid()");
                templates.AddColumn("Name", DataType.NVarChar(255));
                templates.AddColumn("Path", DataType.NVarChar(255));
                templates.AddColumn("UpdatedAt", DataType.DateTime);
                templates.PrimaryKey("ID");
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("Templates");
        }
    }
}
