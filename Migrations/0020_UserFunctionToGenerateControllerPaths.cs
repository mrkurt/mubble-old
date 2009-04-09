using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using ActiveObjects.Creator.Migrations;

namespace Mubble.Migrations
{
    public class UserFunctionToGenerateControllerPaths_20 : ActiveObjects.Creator.Migrations.Base
    {
        public override void MigrateUp()
        {
            using (ActiveFunction func = CreateFunction("ContentPath"))
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
        }

        public override void MigrateDown()
        {
            //Undo your thing
        }
    }
}
