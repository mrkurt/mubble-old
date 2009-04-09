using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class RangeSelectForPostDate_17 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable posts = OpenTable("Posts"))
            {
                posts.AlterColumn("PostDate",
                    delegate(ActiveColumn col)
                    {
                        col.SelectMode = ActiveColumnSelectMode.Range;
                    }
                );
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable posts = OpenTable("Posts"))
            {
                posts.AlterColumn("PostDate",
                    delegate(ActiveColumn col)
                    {
                        col.SelectMode = ActiveColumnSelectMode.Normal;
                    }
                );
            }
        }
    }
}
