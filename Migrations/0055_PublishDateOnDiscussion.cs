using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class PublishDateOnDiscussion_55 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable t = OpenTable("Discussions"))
            {
                t.AddColumn("PublishDate", DataType.DateTime, true, "getdate()");
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable t = OpenTable("Discussions"))
            {
                t.DropColumn("PublishDate");
            }
        }
    }
}
