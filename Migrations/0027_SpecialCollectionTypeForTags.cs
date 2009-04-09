using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class SpecialCollectionTypeForTags_27 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable tags = OpenTable("Tags"))
            {
                tags.SpecializedCollectionType = "MetaDataCollection";
            }
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
