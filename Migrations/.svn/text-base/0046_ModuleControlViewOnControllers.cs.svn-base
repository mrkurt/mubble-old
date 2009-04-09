using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class ModuleControlViewOnControllers_46 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.AddColumn("ModuleControlView", DataType.NVarChar(255), true);
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.DeleteColumn("ModuleControlView");
            }
        }
    }
}
