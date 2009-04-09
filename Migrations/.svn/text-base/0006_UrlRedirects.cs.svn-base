using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class UrlRedirects_6 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            DeleteTableIfExists("UrlRedirects");
            using (ActiveTable redirects = CreateTable("UrlRedirects"))
            {
                redirects.AddColumn("ID", DataType.UniqueIdentifier, false, "newid()");
                redirects.AddColumn("Url", DataType.NVarChar(255));
                redirects.AddColumn("PathExtra", DataType.NVarChar(255));
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("UrlRedirects");
        }
    }
}
