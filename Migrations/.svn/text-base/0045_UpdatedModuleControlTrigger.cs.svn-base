using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class UpdatedModuleControlTrigger_45 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable moduleControls = OpenTable("ModuleControls"))
            {
                moduleControls.DropTriggerIfExists("modulecontrols_trigger_UpdateControllerActiveObjectTypes");
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
    WHERE a.ID = b.ModuleControlID
                    "
                );
            }
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
