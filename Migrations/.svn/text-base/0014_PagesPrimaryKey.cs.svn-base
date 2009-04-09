using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class PagesPrimaryKey_14 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable pages = OpenTable("pages"))
            {
                pages.PrimaryKey("ID");
            }
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
