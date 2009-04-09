using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class LastArticleWithoutTagsSP_65 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            ActiveTable t = OpenTable("Controllers");
            ActiveStoredProcedure next = new ActiveStoredProcedure(t, "controllers_GetNextWithoutTags");

            next.TextBody = @"
    SELECT TOP 1 * FROM Controllers WHERE 
    ActiveObjectType = 'Mubble.Models.Controllers.Article, Mubble.Models' AND
    PublishDate < getDate() AND
    ID NOT IN (SELECT ActiveObjectID FROM Tags WHERE Name='Tag' AND Type='Mubble.Models.Controller')
    ORDER BY PublishDate DESC
";
            next.Save();
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
