using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class CustomPublishStatusAccessors_12 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            foreach(string name in new string[]{"Controllers", "Posts"})
            {
                using(ActiveTable tbl = OpenTable(name))
                {
                    tbl.AlterColumn("Status",
                        delegate(ActiveColumn col)
                        {
                            col.AccessMode = ActiveColumnAccessMode.Custom;
                        }
                    );
                }
            }
        }

        public override void MigrateDown()
        {
            foreach (string name in new string[] { "Controllers", "Posts" })
            {
                using (ActiveTable tbl = OpenTable(name))
                {
                    tbl.AlterColumn("Status",
                        delegate(ActiveColumn col)
                        {
                            col.AccessMode = ActiveColumnAccessMode.Full;
                        }
                    );
                }
            }
        }
    }
}
