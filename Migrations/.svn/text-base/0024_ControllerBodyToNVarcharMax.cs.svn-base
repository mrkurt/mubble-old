using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class ControllerBodyToNVarcharMax_24 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            string[] tables = new string[]{"Controllers", "Pages"};
            foreach(string t in tables)
            {
                using (ActiveTable controllers = OpenTable(t))
                {
                    controllers.AlterColumn("Body",
                        delegate(ActiveColumn col)
                        {
                            col.Type = DataType.NVarCharMax;
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
