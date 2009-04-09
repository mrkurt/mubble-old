using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class ScheduledCommands_57 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            DeleteTableIfExists("ScheduledCommands");
            using (ActiveTable t = CreateTable("ScheduledCommands"))
            {
                t.AddColumn("ID", DataType.UniqueIdentifier, false, "newid()");
                t.AddColumn("RunAt", DataType.DateTime,
                    delegate(ActiveColumn c)
                    {
                        c.SelectMode = ActiveColumnSelectMode.Range;
                    }
                    );
                t.AddColumn("Type", DataType.NVarChar(255), false);
                t.AddColumn("ActiveObjectID", DataType.UniqueIdentifier, false);
                t.AddColumn("Command", DataType.NVarCharMax, false);
                t.PrimaryKey("ID");
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("ScheduledCommands");
        }
    }
}
