using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class BlogPostPagingStoredProcedures_60 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            ActiveTable t = OpenTable("Posts");

            ActiveStoredProcedure next = new ActiveStoredProcedure(t, "posts_GetNext");
            ActiveStoredProcedure prev = new ActiveStoredProcedure(t, "posts_GetPrevious");

            next.AddParameter("@ID", DataType.UniqueIdentifier, null);
            prev.AddParameter("@ID", DataType.UniqueIdentifier, null);

            next.TextBody = prev.TextBody = @"
            DECLARE @ControllerID uniqueidentifier
            DECLARE @PostDate datetime

            SELECT @ControllerID=ControllerID, @PostDate=PostDate FROM Posts WHERE ID=@ID

            IF(@ControllerID IS NOT NULL AND @PostDate IS NOT NULL)
";
            next.TextBody += @"
            SELECT TOP 1 * FROM Posts WHERE PostDate > @PostDate AND ControllerID=@ControllerID ORDER BY PostDate
";
            prev.TextBody += @"
            SELECT TOP 1 * FROM Posts WHERE PostDate < @PostDate AND ControllerID=@ControllerID ORDER BY PostDate DESC
";

            next.Save();
            prev.Save();
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
