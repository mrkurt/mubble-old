using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class ModulePermissionFlags_43 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable permissions = CreateTable("PermissionFlags"))
            {
                permissions.AddColumn("ID", DataType.UniqueIdentifier);
                permissions.AddColumn("Name", DataType.NVarChar(255));
                permissions.AddColumn("Flag", DataType.NVarChar(255));

                permissions.PrimaryKey("ID");
            }

            using (ActiveTable controls = OpenTable("Modules"))
            {
                controls.HasMany("PermissionFlags");
            }

            using (ActiveTable permissions = OpenTable("PermissionFlags"))
            {
                permissions.Unique("ModuleID", "Flag");
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("PermissionFlags");
        }
    }
}
