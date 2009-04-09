using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class ColumnCleanupVariousTables_34 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable posts = OpenTable("Posts"))
            {
                posts.AlterColumn("Excerpt",
                    delegate(ActiveColumn col)
                    {
                        col.Nullable = true;
                    }
                );
            }
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.DropColumn("Depth");
            }
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
