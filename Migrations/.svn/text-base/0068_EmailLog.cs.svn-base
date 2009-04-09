using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class EmailLog_68 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveTable t = CreateTable("SentEmails"))
            {
                t.AddColumn("ID", DataType.UniqueIdentifier, false, "newid()");
                t.AddColumn("SenderIP", DataType.VarChar(16), false);
                t.AddColumn("SenderName", DataType.NVarChar(255), true);
                t.AddColumn("SenderEmail", DataType.NVarChar(255), false);
                t.AddColumn("RecipientEmail", DataType.NVarChar(255), false);
                t.AddColumn("SentAt", DataType.DateTime,
                    delegate(ActiveColumn col)
                    {
                        col.SelectMode = ActiveColumnSelectMode.Range;
                    }
                );
                t.AddColumn("Message", DataType.NVarCharMax, true);
                t.AddColumn("IsSpam", DataType.Bit, false, "0");
                t.PrimaryKey("ID");
                t.Index("SenderIP");
                t.Index("SenderIP", "IsSpam");
            }
        }

        public override void MigrateDown()
        {
            DeleteTableIfExists("SentEmails");
        }
    }
}
