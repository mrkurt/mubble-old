using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class ContentTypeFlagsOnControllers_37 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.AddColumn("IsContent", DataType.Bit, true, "0");
                controllers.AddColumn("IsContentContainer", DataType.Bit, true, "0");
            }
        }

        public override void MigrateDown()
        {
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.DropColumn("IsContent");
                controllers.DropColumn("IsContentContainer");
            }
        }
    }
}
