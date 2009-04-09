using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class SaneControllerSort_74 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable t = OpenTable("Controllers"))
            {
                t.DefaultOrderBy = "PublishDate DESC";
                t.Index("PublishDate DESC");

                t.Index("ControllerID");
            }
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
