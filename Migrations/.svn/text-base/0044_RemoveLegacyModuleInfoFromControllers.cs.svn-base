using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class RemoveLegacyModuleInfoFromControllers_44 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.DeleteColumn("ModuleControl");
                controllers.DeleteColumn("ModuleID");
            }
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
