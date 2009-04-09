using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class ModuleControlsRelationshipWithControllers_40 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.AddColumn("ModuleControlID", DataType.UniqueIdentifier, true);
            }
            Execute(@"
                UPDATE 
                    Controllers 
                SET 
                    ModuleControlID=b.ID 
                FROM 
                    Controllers a, 
                    ModuleControls b
                WHERE 
                    a.ModuleID=b.ModuleID AND
                    a.ModuleControl = b.FileName
            ");

            using (ActiveTable controls = OpenTable("ModuleControls"))
            {
                controls.HasMany("Controllers", "ModuleControlID");
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.DeleteColumn("ModuleControlID");
            }
        }
    }
}
