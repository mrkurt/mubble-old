using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class TagIndexForSearching_26 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable tags = OpenTable("Tags"))
            {
                tags.AddColumn("ActiveObjectID", DataType.UniqueIdentifier);
                tags.Index("Type", "ActiveObjectID");
            }
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
