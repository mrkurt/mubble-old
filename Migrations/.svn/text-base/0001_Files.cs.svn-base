using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class Files_1 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            DeleteTableIfExists("Files");
            using (ActiveTable media = CreateTable("Files"))
            {
                media.AddColumn("ID", DataType.UniqueIdentifier, false, "newid()");
                media.AddColumn("Name", DataType.NVarChar(255));
                media.AddColumn("FileName", DataType.NVarChar(255));
                media.AddColumn("PhysicalPath", DataType.NVarChar(255));
                media.AddColumn("MediaType", DataType.NVarChar(50));
                media.AddColumn("MediaSubType", DataType.NVarChar(50));
                media.AddColumn("UpdatedAt", DataType.DateTime, true, "getdate()");
                media.PrimaryKey("ID");
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("Files");
        }
    }
}
