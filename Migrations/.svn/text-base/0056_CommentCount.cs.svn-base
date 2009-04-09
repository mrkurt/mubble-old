using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class CommentCount_56 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable t = OpenTable("Discussions"))
            {
                t.AddColumn("CommentCount", DataType.Int, false, "0");
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable t = OpenTable("Discussions"))
            {
                t.DeleteColumn("CommentCount");
            }
        }
    }
}
