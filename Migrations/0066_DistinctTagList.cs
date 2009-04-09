using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class DistinctTagList_66 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            ActiveTable t = OpenTable("Tags");
            ActiveStoredProcedure next = new ActiveStoredProcedure(t, "tags_GetDistinctStringValues");

            next.AddParameter("@Name", DataType.NVarChar(255));
            next.TextBody = @"
select top 50 count(id) as NumericValue, Name, StringValue, StringValueNormalized from Tags where Name=@Name
group by name, stringvalue, stringvaluenormalized
order by count(id) desc
";
            next.Save();
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
