using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class FixXmlColumnOnControllers_31 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.AlterColumn("Settings",
                    delegate(ActiveColumn col){
                        col.Type = DataType.NVarCharMax;
                    }
                );
            }
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
