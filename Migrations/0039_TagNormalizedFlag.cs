using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class TagNormalizedFlag_39 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable tags = OpenTable("Tags"))
            {
                tags.AddColumn("NormalizeStringValue", DataType.Bit, false, "1");
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable tags = OpenTable("Tags"))
            {
                tags.DeleteColumn("NormalizeStringValue");
            }
        }
    }
}
