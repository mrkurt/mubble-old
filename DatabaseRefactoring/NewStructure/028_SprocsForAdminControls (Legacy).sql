/****** Object:  StoredProcedure [dbo].[admincontrols_Delete]    Script Date: 01/13/2008 23:01:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[admincontrols_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[AdminControls]
	WHERE
		[ID]=@Id

GO
/****** Object:  StoredProcedure [dbo].[admincontrols_List]    Script Date: 01/13/2008 23:01:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[admincontrols_List]
	@Id [uniqueidentifier] = null,
	@Name [nvarchar](255) = null,
	@FileName [nvarchar](255) = null,
	@Order [int] = null,
	@IsDefault [bit] = null,
	@ModuleControlID [uniqueidentifier] = null
AS

	SELECT
		[ID],
		[Name],
		[FileName],
		[Order],
		[IsDefault],
		[ModuleControlID]
	FROM
		dbo.AdminControls
	WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		([Name] LIKE @Name OR @Name IS NULL)
		AND
		([FileName] LIKE @FileName OR @FileName IS NULL)
		AND
		([Order] = @Order OR @Order IS NULL)
		AND
		([IsDefault] = @IsDefault OR @IsDefault IS NULL)
		AND
		([ModuleControlID] = @ModuleControlID OR @ModuleControlID IS NULL)

GO

/****** Object:  StoredProcedure [dbo].[admincontrols_Save]    Script Date: 01/13/2008 23:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[admincontrols_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@Name [nvarchar](255) = null OUTPUT,
	@FileName [nvarchar](255) = null OUTPUT,
	@Order [int] = null OUTPUT,
	@IsDefault [bit] = null OUTPUT,
	@ModuleControlID [uniqueidentifier] = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM AdminControls WHERE [ID] = @Id)
		INSERT INTO AdminControls([ID], [Name], [FileName], [Order], [IsDefault], [ModuleControlID]) 
        VALUES (@Id, @Name, @FileName, @Order, @IsDefault, @ModuleControlID)
	ELSE
		UPDATE
			AdminControls
		SET
			[ID] = @Id,
			[Name] = @Name,
			[FileName] = @FileName,
			[Order] = @Order,
			[IsDefault] = @IsDefault,
			[ModuleControlID] = @ModuleControlID
		WHERE
			[ID] = @Id
GO