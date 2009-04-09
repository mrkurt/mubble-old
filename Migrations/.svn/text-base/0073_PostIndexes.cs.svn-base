using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class PostIndexes_73 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable t = OpenTable("Posts"))
            {
                t.Index("PublishDate DESC");
                t.Index("ControllerID");
                //t.Index("ControllerID", "Slug");
            }
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
