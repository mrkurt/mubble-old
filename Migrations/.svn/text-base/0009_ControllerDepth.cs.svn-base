using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class ControllerDepth_9 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.AddColumn("Depth", DataType.Int, false, "'0'");
            }
        }

        public override void MigrateDown()
        {
            using(ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.DropColumn("Depth");
            }
        }
    }
}
