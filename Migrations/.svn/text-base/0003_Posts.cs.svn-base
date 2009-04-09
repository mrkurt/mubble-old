using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class PostsAndPages_3 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            DeleteTableIfExists("Posts");
            using (ActiveTable posts = CreateTable("Posts"))
            {
                posts.AddColumn("ID", DataType.UniqueIdentifier, false, "newid()");
                posts.AddColumn("Slug", DataType.NVarChar(255));
                posts.AddColumn("UserName", DataType.NVarChar(255));
                posts.AddColumn("Title", DataType.NVarChar(255));
                posts.AddColumn("Excerpt", DataType.NVarCharMax);
                posts.AddColumn("Body", DataType.NVarCharMax);
                posts.AddColumn("PostDate", DataType.DateTime);
                posts.AddColumn("Status", DataType.Int);
                posts.AddColumn("DiscussionUrl", DataType.NVarChar(255));
                posts.AddColumn("MoreUrl", DataType.NVarChar(255));
                posts.PrimaryKey("ID");
            }

            DeleteTableIfExists("Pages");
            using (ActiveTable pages = CreateTable("Pages"))
            {
                pages.AddColumn("ID", DataType.UniqueIdentifier, false, "newid()");
                pages.AddColumn("PageNumber", DataType.Int);
                pages.AddColumn("Name", DataType.NVarChar(255));
                pages.AddColumn("Body", DataType.NVarCharMax);
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("Posts");
            DeleteTableIfExists("Pages");
        }
    }
}
