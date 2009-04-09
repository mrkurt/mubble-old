using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class AdditionalRouteFields_52 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable t = OpenTable("Routes"))
            {
                t.AddColumn("IsDefault", DataType.Bit, false, "0");
                t.AddColumn("Name", DataType.NVarChar(255), true);
                t.AddColumn("FormatString", DataType.NVarChar(255), true);

                t.HasMany("Controllers");
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable t = OpenTable("Routes"))
            {
                t.DropColumn("IsDefault");
                t.DropColumn("Name");
                t.DropColumn("FormatString");
            }
        }
    }
}
