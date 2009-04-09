using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class CustomerAccessorForTagStringValue_28 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable tags = OpenTable("Tags"))
            {
                tags.AlterColumn("StringValue",
                    delegate(ActiveColumn col)
                    {
                        col.AccessMode = ActiveColumnAccessMode.Custom;
                    }
                );
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable tags = OpenTable("Tags"))
            {
                tags.AlterColumn("StringValue",
                    delegate(ActiveColumn col)
                    {
                        col.AccessMode = ActiveColumnAccessMode.Full;
                    }
                );
            }
        }
    }
}
