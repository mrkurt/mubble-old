using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class RangeSelectForControllerDepth2_19 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.AlterColumn("Depth",
                    delegate(ActiveColumn col)
                    {
                        col.SelectMode = ActiveColumnSelectMode.Range;
                    }
                );
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.AlterColumn("Depth",
                    delegate(ActiveColumn col)
                    {
                        col.SelectMode = ActiveColumnSelectMode.Normal;
                    }
                );
            }
        }
    }
}
