using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class TriggersToClearTagsAndPermissions_30 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.AddTrigger("controllers_trigger_ClearTags",
                    delegate(ActiveTrigger trigger)
                    {
                        trigger.ForDelete = true;
                        trigger.ForInsert = false;
                        trigger.ForUpdate = false;
                    },
                    @"
    SET NOCOUNT ON;
    DELETE FROM Tags WHERE ActiveObjectID IN (SELECT ID FROM Deleted) AND Type='Mubble.Models.Controller'
                    "
                );

                controllers.AddTrigger("controllers_trigger_ClearPermissions",
                    delegate(ActiveTrigger trigger)
                    {
                        trigger.ForDelete = true;
                        trigger.ForInsert = false;
                        trigger.ForUpdate = false;
                    },
                    @"
    SET NOCOUNT ON;
    DELETE FROM Permissions WHERE ActiveObjectID IN (SELECT ID FROM Deleted) AND Type='Mubble.Models.Controller'
                    "
                );
            }

            using (ActiveTable posts = OpenTable("Posts"))
            {
                posts.AddTrigger("posts_trigger_ClearTags",
                    delegate(ActiveTrigger trigger)
                    {
                        trigger.ForDelete = true;
                        trigger.ForInsert = false;
                        trigger.ForUpdate = false;
                    },
                    @"
    SET NOCOUNT ON;
    DELETE FROM Tags WHERE ActiveObjectID IN (SELECT ID FROM Deleted) AND Type='Mubble.Models.Controller'
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
