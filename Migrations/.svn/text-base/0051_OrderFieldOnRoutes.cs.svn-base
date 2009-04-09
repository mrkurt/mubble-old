using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class OrderFieldOnRoutes_51 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable t = OpenTable("Routes"))
            {
                t.AddColumn("Order", DataType.Int, true);
                t.DefaultOrderBy = "[Order]";
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable t = OpenTable("Routes"))
            {
                t.DropColumn("Order");
                t.DefaultOrderBy = null;
            }
        }
    }
}
