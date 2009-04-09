using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class OrderByPostDateOnPosts_47 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable posts = OpenTable("Posts"))
            {
                posts.DefaultOrderBy = "PostDate DESC";
            }
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
