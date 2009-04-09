using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class HandlerOnRedirects_62 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable t = OpenTable("UrlRedirects"))
            {
                t.AddColumn("Handler", DataType.NVarChar(255), true);
                t.Unique("Url");
                t.PrimaryKey("ID");
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable t = OpenTable("UrlRedirects"))
            {
                t.DeleteColumn("Handler");
            }
        }
    }
}
