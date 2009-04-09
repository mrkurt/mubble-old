using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class Tags_13 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            DeleteTableIfExists("Tags");
            using (ActiveTable tags = CreateTable("Tags"))
            {
                tags.AddColumn("ID", DataType.UniqueIdentifier, false, "newid()");
                tags.AddColumn("Type", DataType.NVarChar(255), false);
                tags.AddColumn("Name", DataType.NVarChar(255), false);
                tags.AddColumn("StringValue", DataType.NVarChar(255), true);
                tags.AddColumn("StringValueNormalized", DataType.NVarChar(255), true);
                tags.AddColumn("NumericValue", DataType.Float);
                tags.PrimaryKey("ID");
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("Tags");
        }
    }
}
