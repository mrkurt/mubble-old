using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class IntelligentNullsForPosts_25 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable posts = OpenTable("Posts"))
            {
                string[] cols = new string[] { "Excerpt", "MoreURL", "DiscussionUrl", "Body", "UserName" };
                foreach (string c in cols)
                {
                    posts.AlterColumn(c,
                        delegate(ActiveColumn col)
                        {
                            col.Nullable = true;
                        }
                    );
                }

                cols = new string[]{"Body", "Excerpt"};

                foreach (string c in cols)
                {
                    posts.AlterColumn(c,
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
