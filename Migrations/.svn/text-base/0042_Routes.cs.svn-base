using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class Routes_42 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable routes = CreateTable("Routes"))
            {
                routes.AddColumn("ID", DataType.UniqueIdentifier);
                routes.AddColumn("Pattern", DataType.NVarCharMax);

                routes.PrimaryKey("ID");
            }

            using (ActiveTable moduleControls = OpenTable("ModuleControls"))
            {
                moduleControls.HasMany("Routes");
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("Routes");
        }
    }
}
