using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class RenamedColumnsForConsistentContent_69 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable t = OpenTable("Controllers"))
            {
                t.RenameColumn("Name", "Title");
            }
            using (ActiveTable t = OpenTable("Posts"))
            {
                t.RenameColumn("PostDate", "PublishDate");
                t.DefaultOrderBy = "PublishDate DESC";
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable t = OpenTable("Controllers"))
            {
                t.RenameColumn("Title", "Name");
            }
            using (ActiveTable t = OpenTable("Posts"))
            {
                t.RenameColumn("PublishDate", "PostDate");
                t.DefaultOrderBy = "PostDate DESC";
            }
        }
    }
}
