using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class SummaryFieldsOnControllers_32 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.AddColumn("Excerpt", DataType.NVarCharMax, true);
                controllers.AddColumn("Summary", DataType.NVarCharMax, true);
            }
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
