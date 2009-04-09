using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class DefaultSortForFiles_71 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable t = OpenTable("Files"))
            {
                t.DefaultOrderBy = "UpdatedAt DESC";
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable t = OpenTable("Files"))
            {
                t.DefaultOrderBy = "FileName";
            }
        }
    }
}
