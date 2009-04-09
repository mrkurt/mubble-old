using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class IntelligentControllerNulls_22 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                string[] cols = new string[] { "Body", "UpdatedAt", "ControllerID" };
                foreach (string c in cols)
                {
                    controllers.AlterColumn(c,
                        delegate(ActiveColumn col)
                        {
                            col.Nullable = true;
                        }
                    );
                }
            }
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
