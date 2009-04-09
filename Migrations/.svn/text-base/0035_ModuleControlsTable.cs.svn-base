using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class ModuleControlsTable_35 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            DeleteTableIfExists("ModuleControls");
            using (ActiveTable controls = CreateTable("ModuleControls"))
            {
                controls.AddColumn("ID", DataType.UniqueIdentifier, false, "newid()");
                controls.AddColumn("Order", DataType.Int, true);
                controls.AddColumn("Name", DataType.NVarChar(255), false);
                controls.AddColumn("FileName", DataType.NVarChar(255), false);
                controls.AddColumn("Type", DataType.NVarChar(255), true);
                controls.PrimaryKey("ID");
            }
            using (ActiveTable modules = OpenTable("Modules"))
            {
                modules.HasMany("ModuleControls");
            }
            using (ActiveTable controls = OpenTable("ModuleControls"))
            {
                controls.Unique("ModuleID", "FileName");
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("ModuleControls");
        }
    }
}
