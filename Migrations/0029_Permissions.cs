using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class Permissions_29 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            DeleteTableIfExists("Permissions");
            using (ActiveTable permissions = CreateTable("Permissions"))
            {
                permissions.AddColumn("ID", DataType.UniqueIdentifier, false, "newid()");
                permissions.AddColumn("Type", DataType.NVarChar(255), false);
                permissions.AddColumn("ActiveObjectID", DataType.UniqueIdentifier);
                permissions.AddColumn("Group", DataType.NVarChar(255), false);
                permissions.AddColumn("Flag", DataType.NVarChar(255), false);
                permissions.PrimaryKey("ID");

                permissions.SpecializedCollectionType = "PermissionsCollection";

                permissions.Index("Type", "ActiveObjectID");
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("Permissions");
        }
    }
}
