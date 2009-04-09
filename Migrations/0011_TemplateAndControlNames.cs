using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class TemplateAndControlNames_11 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.AddColumn("ModuleControl", DataType.NVarChar(255));
                controllers.AddColumn("TemplateControl", DataType.NVarChar(255));
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.DeleteColumn("ModuleControl");
                controllers.DeleteColumn("TemplateControl");
            }
        }
    }
}
