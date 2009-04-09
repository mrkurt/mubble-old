using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class CustomUrlRedirectSaveFunction_63 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable t = OpenTable("UrlRedirects"))
            {
                t.UseCustomSaveFunction = true;
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable t = OpenTable("UrlRedirects"))
            {
                t.UseCustomSaveFunction = false;
            }
        }
    }
}
