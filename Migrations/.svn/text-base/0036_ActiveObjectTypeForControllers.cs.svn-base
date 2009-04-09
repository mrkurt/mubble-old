using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class ActiveObjectTypeForControllers_36 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.AddColumn("ActiveObjectType", DataType.NVarChar(255), true);
            }
            using (ActiveTable moduleControls = OpenTable("ModuleControls"))
            {
                moduleControls.AddColumn("ControllerActiveObjectType", DataType.NVarChar(255), true);
                moduleControls.AddTrigger("modulecontrols_trigger_UpdateControllerActiveObjectTypes",
                    delegate(ActiveTrigger trigger)
                    {
                        trigger.ForDelete = false;
                        trigger.ForInsert = true;
                        trigger.ForUpdate = true;
                    },
                    @"
    SET NOCOUNT ON;
    UPDATE Controllers SET ActiveObjectType=a.ControllerActiveObjectType FROM Inserted a, Controllers b
    WHERE a.ModuleID = b.ModuleID AND a.FileName = b.ModuleControl
                    "
                );
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.DropColumn("ActiveObjectType");
            }
            using (ActiveTable moduleControls = OpenTable("ModuleControls"))
            {
                moduleControls.DropColumn("ControllerActiveObjectType");
                moduleControls.DropTriggerIfExists("modulecontrols_trigger_UpdateControllerActiveObjectTypes");
            }
        }
    }
}
