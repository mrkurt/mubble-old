using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class SortingOnPages_50 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable t = OpenTable("Pages"))
            {
                t.DefaultOrderBy = "PageNumber";
            }
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
