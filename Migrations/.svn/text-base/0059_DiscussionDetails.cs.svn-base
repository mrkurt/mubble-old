using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class DiscussionDetails_59 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable t = OpenTable("Discussions"))
            {
                t.AddColumn("Title", DataType.NVarChar(255), true);
                t.AddColumn("Excerpt", DataType.NVarCharMax, true);
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable t = OpenTable("Discussions"))
            {
                t.DropColumn("Title");
                t.DropColumn("Excerpt");
            }
        }
    }
}
