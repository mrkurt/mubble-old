using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class LastPostWithoutTagsSP_64 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            ActiveTable t = OpenTable("Posts");
            ActiveStoredProcedure next = new ActiveStoredProcedure(t, "posts_GetNextWithoutTags");

            next.TextBody = @"
    SELECT TOP 1 * FROM Posts WHERE 
    PostDate < getDate() AND
    ID NOT IN (SELECT ActiveObjectID FROM Tags WHERE Name='Tag' AND Type='Mubble.Models.Post')
    ORDER BY PostDate DESC
";
            next.Save();
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
