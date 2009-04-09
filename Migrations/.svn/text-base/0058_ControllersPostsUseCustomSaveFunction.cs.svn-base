using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class ControllersPostsUseCustomSaveFunction_58 : ActiveObjects.Creator.Migrations.Base
    {
        string[] tables = new string[] { "Controllers", "Posts" };
        public override void MigrateUp()
        {            
            foreach (string table in tables)
            {
                using (ActiveTable t = OpenTable(table))
                {
                    t.UseCustomSaveFunction = true;
                }
            }
        }

        public override void MigrateDown()
        {
            foreach (string table in tables)
            {
                using (ActiveTable t = OpenTable(table))
                {
                    t.UseCustomSaveFunction = false;
                }
            }
        }
    }
}
