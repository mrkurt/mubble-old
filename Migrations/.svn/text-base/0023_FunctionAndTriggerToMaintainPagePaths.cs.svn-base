using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class FunctionAndTriggerToMaintainPagePaths_23 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            DropFunctionIfExists("ContentPath");

            using (ActiveFunction func = CreateFunction("ControllerPath"))
            {
                func.Type = UserDefinedFunctionType.Scalar;
                func.ReturnValueDataType = DataType.NVarChar(1000);
                func.AddParameter("@ControllerID", DataType.UniqueIdentifier);
                func.TextBody = @"
BEGIN
DECLARE @Path nvarchar(255), @FileName nvarchar(255) 
DECLARE @ParentID uniqueidentifier
SET @Path = ''

SELECT @Path = '/' + FileName, @ParentID = ControllerID 
FROM Controllers
WHERE ID = @ControllerID 

WHILE @ParentID IS NOT NULL 
BEGIN
    SELECT @FileName = FileName, @ParentId = ControllerId
    FROM Controllers
    WHERE ID = @ParentID

    IF @ParentID IS NULL
        SET @FileName = ''
    ELSE
        SET @FileName = '/' + @FileName
    
    SET @Path = @FileName + @Path
END

RETURN @Path
END
                ";
            }

            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.DropTriggerIfExists("controllers_trigger_SyncPaths");

                controllers.AddTrigger("controllers_trigger_SyncPaths",
                    delegate(ActiveTrigger trigger)
                    {
                        trigger.ForInsert = true;
                        trigger.ForUpdate = true;
                    },
                    @"
SET NOCOUNT ON;
SELECT ID, 0 as Included 
INTO #PathsToUpdate 
FROM Inserted

WHILE EXISTS( SELECT ID FROM #PathsToUpdate WHERE Included = 0)
	BEGIN
		
		INSERT INTO #PathsToUpdate (ID, Included)
		SELECT ID, -1 
		FROM Controllers 
		WHERE ControllerID IN (
				SELECT ID 
				FROM #PathsToUpdate 
				WHERE ControllerID IN (
					SELECT ID 
					FROM #PathsToUpdate 
					WHERE Included = 0
				)
			)
		UPDATE #PathsToUpdate SET Included = Included + 1
	END

UPDATE Controllers SET Path=dbo.ControllerPath(ID) WHERE ID IN (SELECT ID FROM #PathsToUpdate)
                    "
                );
            }
        }

        public override void MigrateDown()
        {
            DropFunctionIfExists("RecursiveControllerPath");
            using (ActiveTable controllers = OpenTable("Controllers"))
            {
                controllers.DropTriggerIfExists("controllers_trigger_SyncPaths");
            }
        }
    }
}
