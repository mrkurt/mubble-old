using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class Controllers_7 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            DeleteTableIfExists("Controllers");
            using (ActiveTable controllers = CreateTable("Controllers"))
            {
                controllers.AddColumn("ID", DataType.UniqueIdentifier, false, "newid()");
                controllers.AddColumn("Name", DataType.NVarChar(255));
                controllers.AddColumn("FileName", DataType.NVarChar(255));
                controllers.AddColumn("Body", DataType.NVarCharMax);
                controllers.AddColumn("Path", DataType.NVarChar(255));
                controllers.AddColumn("PublishDate", DataType.DateTime, false, "getdate()");
                controllers.AddColumn("ModifyDate", DataType.DateTime, false, "getdate()");

                controllers.AddColumn("Settings", DataType.Xml(""),
                    delegate(ActiveColumn col)
                    {
                        col.AccessMode = ActiveColumnAccessMode.Custom;
                        col.Nullable = true;
                    }
                    );

                controllers.AddColumn("Status", DataType.Int);
                controllers.AddColumn("UpdatedAt", DataType.DateTime);

                controllers.PrimaryKey("ID");

                controllers.HasMany("Files");
                controllers.HasMany("Posts");
                controllers.HasMany("Pages");
                controllers.HasMany("UrlRedirects");
            }

            using (ActiveTable modules = OpenTable("Modules"))
            {
                modules.HasMany("Controllers");
            }
            using (ActiveTable templates = OpenTable("Templates"))
            {
                templates.HasMany("Controllers");
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("Controllers");
        }
    }
}
