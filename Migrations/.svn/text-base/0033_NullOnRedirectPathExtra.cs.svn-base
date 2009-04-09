using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class NullOnRedirectPathExtra_33 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable redirects = OpenTable("UrlRedirects"))
            {
                redirects.AlterColumn("PathExtra",
                    delegate(ActiveColumn col)
                    {
                        col.Nullable = true;
                    }
                );
            }
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
