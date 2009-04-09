using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class ControllerPathChanges_8 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.AlterColumn("Path",
                    delegate(ActiveColumn col)
                    {
                        col.AccessMode = ActiveColumnAccessMode.ReadOnly;
                        col.DefaultValue = "newid()";
                    }
                );
                controllers.Unique("Path");
            }
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
