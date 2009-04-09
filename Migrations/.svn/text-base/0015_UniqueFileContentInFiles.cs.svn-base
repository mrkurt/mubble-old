using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class UniqueFileContentInFiles_15 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable files = OpenTable("Files"))
            {
                files.Unique("ControllerId", "FileName");
            }
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
