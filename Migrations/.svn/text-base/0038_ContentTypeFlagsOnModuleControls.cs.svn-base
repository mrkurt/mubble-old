using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class ContentTypeFlagsOnModuleControls_38 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable moduleControls = OpenTable("ModuleControls"))
            {
                moduleControls.AddColumn("IsContent", DataType.Bit, true, "0");
                moduleControls.AddColumn("IsContentContainer", DataType.Bit, true, "0");

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
                    UPDATE Controllers SET ActiveObjectType=a.ControllerActiveObjectType, IsContent=a.IsContent, IsContentContainer=a.IsContentContainer
                    FROM Inserted a, Controllers b
                    WHERE a.ModuleID = b.ModuleID AND a.FileName = b.ModuleControl
                                    "
                );
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable moduleControls = OpenTable("ModuleControls"))
            {
                moduleControls.DropColumn("IsContent");
                moduleControls.DropColumn("IsContentContainer");
            }
        }
    }
}
