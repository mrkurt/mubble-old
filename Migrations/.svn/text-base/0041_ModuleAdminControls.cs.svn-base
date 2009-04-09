using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class ModuleAdminControls_41 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable controls = CreateTable("AdminControls"))
            {
                controls.AddColumn("ID", DataType.UniqueIdentifier);
                controls.AddColumn("Name", DataType.NVarChar(255));
                controls.AddColumn("FileName", DataType.NVarChar(255));
                controls.AddColumn("Order", DataType.Int);
                controls.AddColumn("IsDefault", DataType.Bit);

                controls.PrimaryKey("ID");
            }
            using (ActiveTable controls = OpenTable("ModuleControls"))
            {
                controls.HasMany("AdminControls");
            }

            using (ActiveTable controls = OpenTable("AdminControls"))
            {
                controls.Unique("ModuleControlID", "FileName");
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("AdminControls");
        }
    }
}
