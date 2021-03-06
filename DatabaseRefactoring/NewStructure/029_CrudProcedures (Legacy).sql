/****** Object:  StoredProcedure [dbo].[modules_Delete]    Script Date: 01/13/2008 23:21:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[modules_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[Modules]
	WHERE
		[ID]=@Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'04B1A90FBA216E6E02B2BC3E2FE8F98E' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'modules_Delete'
GO
/****** Object:  StoredProcedure [dbo].[modules_Save]    Script Date: 01/13/2008 23:21:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[modules_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@Name [nvarchar](255) = null OUTPUT,
	@Path [nvarchar](255) = null OUTPUT,
	@UpdatedAt [datetime] = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM Modules WHERE [ID] = @Id)
		INSERT INTO Modules([ID], [Name], [Path], [UpdatedAt]) 
        VALUES (@Id, @Name, @Path, @UpdatedAt)
	ELSE
		UPDATE
			Modules
		SET
			[ID] = @Id,
			[Name] = @Name,
			[Path] = @Path,
			[UpdatedAt] = @UpdatedAt
		WHERE
			[ID] = @Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'C50E2DFC20DB9B8951C271CAD9B86C89' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'modules_Save'
GO
/****** Object:  StoredProcedure [dbo].[modules_List]    Script Date: 01/13/2008 23:21:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[modules_List]
	@Id [uniqueidentifier] = null,
	@Name [nvarchar](255) = null,
	@Path [nvarchar](255) = null,
	@UpdatedAt [datetime] = null
AS

	SELECT
		[ID],
		[Name],
		[Path],
		[UpdatedAt]
	FROM
		dbo.Modules
	WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		([Name] LIKE @Name OR @Name IS NULL)
		AND
		([Path] LIKE @Path OR @Path IS NULL)
		AND
		([UpdatedAt] = @UpdatedAt OR @UpdatedAt IS NULL)
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'FAD7267A0F3BB0EB118513D69C3A5B39' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'modules_List'
GO
/****** Object:  StoredProcedure [dbo].[discussionproviders_Save]    Script Date: 01/13/2008 23:21:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[discussionproviders_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@Name [nvarchar](255) = null OUTPUT,
	@ActiveObjectType [nvarchar](255) = null OUTPUT,
	@Settings [nvarchar](max) = null OUTPUT,
	@IsDefault [bit] = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM DiscussionProviders WHERE [ID] = @Id)
		INSERT INTO DiscussionProviders([ID], [Name], [ActiveObjectType], [Settings], [IsDefault]) 
        VALUES (@Id, @Name, @ActiveObjectType, @Settings, @IsDefault)
	ELSE
		UPDATE
			DiscussionProviders
		SET
			[ID] = @Id,
			[Name] = @Name,
			[ActiveObjectType] = @ActiveObjectType,
			[Settings] = @Settings,
			[IsDefault] = @IsDefault
		WHERE
			[ID] = @Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'900EC2CBDAF0EFAE3DED409FA6110B3B' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'discussionproviders_Save'
GO
/****** Object:  StoredProcedure [dbo].[discussionproviders_List]    Script Date: 01/13/2008 23:21:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[discussionproviders_List]
	@Id [uniqueidentifier] = null,
	@Name [nvarchar](255) = null,
	@ActiveObjectType [nvarchar](255) = null,
	@Settings [nvarchar](max) = null,
	@IsDefault [bit] = null,
	@RowIndex_start [bigint] = null,
	@RowIndex_end [bigint] = null,
	@RowIndex_total [bigint] = null OUTPUT
AS

    IF @RowIndex_start IS NULL OR @RowIndex_start < 0
        SET @RowIndex_start = 0

    SELECT
        ROW_NUMBER() OVER(ORDER BY ID) as RowNumberColumn, ID
    INTO
        #results
	FROM
		dbo.DiscussionProviders
	WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		([Name] LIKE @Name OR @Name IS NULL)
		AND
		([ActiveObjectType] LIKE @ActiveObjectType OR @ActiveObjectType IS NULL)
		AND
		([Settings] LIKE @Settings OR @Settings IS NULL)
		AND
		([IsDefault] = @IsDefault OR @IsDefault IS NULL)


    CREATE UNIQUE CLUSTERED INDEX
        idx_uc_rownum
    ON
        #results(RowNumberColumn)

    
    SELECT @RowIndex_total = COUNT(RowNumberColumn) FROM #results

    IF @RowIndex_end <= 0
        SET @RowIndex_end = @RowIndex_total + 1

    IF @RowIndex_end IS NULL OR @RowIndex_end < @RowIndex_start
        SET @RowIndex_end = @RowIndex_start + 1000    

    SELECT * FROM DiscussionProviders WHERE ID IN
    (SELECT ID FROM #results WHERE RowNumberColumn BETWEEN @RowIndex_start AND (@RowIndex_end - 1))
    ORDER BY
        ID

    DROP TABLE #results
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'06DBB41F2B86EC68013D8CBF56EE5FDF' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'discussionproviders_List'
GO
/****** Object:  StoredProcedure [dbo].[discussionproviders_Delete]    Script Date: 01/13/2008 23:21:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[discussionproviders_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[DiscussionProviders]
	WHERE
		[ID]=@Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'179F4B5186F72EF475253816F1AAE21C' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'discussionproviders_Delete'
GO
/****** Object:  StoredProcedure [dbo].[admincontrols_Delete]    Script Date: 01/13/2008 23:21:02 ******/
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
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'DDF3616AF9AD96351AA53C3C29DD50DB' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'admincontrols_Delete'
GO
/****** Object:  StoredProcedure [dbo].[admincontrols_Save]    Script Date: 01/13/2008 23:21:03 ******/
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
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'D81E90D751DADAF7A4BC0E79D92CB438' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'admincontrols_Save'
GO
/****** Object:  StoredProcedure [dbo].[admincontrols_List]    Script Date: 01/13/2008 23:21:03 ******/
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
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'5A208F947FD2D909550923781D630910' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'admincontrols_List'
GO
/****** Object:  StoredProcedure [dbo].[modulecontrols_Delete]    Script Date: 01/13/2008 23:21:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[modulecontrols_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[ModuleControls]
	WHERE
		[ID]=@Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'ECA162C28096354B319603DDEDFA4AB6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'modulecontrols_Delete'
GO
/****** Object:  StoredProcedure [dbo].[modulecontrols_Save]    Script Date: 01/13/2008 23:21:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[modulecontrols_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@Order [int] = null OUTPUT,
	@Name [nvarchar](255) = null OUTPUT,
	@FileName [nvarchar](255) = null OUTPUT,
	@Type [nvarchar](255) = null OUTPUT,
	@ModuleID [uniqueidentifier] = null OUTPUT,
	@ControllerActiveObjectType [nvarchar](255) = null OUTPUT,
	@IsContent [bit] = null OUTPUT,
	@IsContentContainer [bit] = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM ModuleControls WHERE [ID] = @Id)
		INSERT INTO ModuleControls([ID], [Order], [Name], [FileName], [Type], [ModuleID], [ControllerActiveObjectType], [IsContent], [IsContentContainer]) 
        VALUES (@Id, @Order, @Name, @FileName, @Type, @ModuleID, @ControllerActiveObjectType, @IsContent, @IsContentContainer)
	ELSE
		UPDATE
			ModuleControls
		SET
			[ID] = @Id,
			[Order] = @Order,
			[Name] = @Name,
			[FileName] = @FileName,
			[Type] = @Type,
			[ModuleID] = @ModuleID,
			[ControllerActiveObjectType] = @ControllerActiveObjectType,
			[IsContent] = @IsContent,
			[IsContentContainer] = @IsContentContainer
		WHERE
			[ID] = @Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'3C998E5C6A07F6E559505CF076DF21B8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'modulecontrols_Save'
GO
/****** Object:  StoredProcedure [dbo].[modulecontrols_List]    Script Date: 01/13/2008 23:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[modulecontrols_List]
	@Id [uniqueidentifier] = null,
	@Order [int] = null,
	@Name [nvarchar](255) = null,
	@FileName [nvarchar](255) = null,
	@Type [nvarchar](255) = null,
	@ModuleID [uniqueidentifier] = null,
	@ControllerActiveObjectType [nvarchar](255) = null,
	@IsContent [bit] = null,
	@IsContentContainer [bit] = null,
	@RowIndex_start [bigint] = null,
	@RowIndex_end [bigint] = null,
	@RowIndex_total [bigint] = null OUTPUT
AS

    IF @RowIndex_start IS NULL OR @RowIndex_start < 0
        SET @RowIndex_start = 0

    SELECT
        ROW_NUMBER() OVER(ORDER BY ID) as RowNumberColumn, ID
    INTO
        #results
	FROM
		dbo.ModuleControls
	WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		([Order] = @Order OR @Order IS NULL)
		AND
		([Name] LIKE @Name OR @Name IS NULL)
		AND
		([FileName] LIKE @FileName OR @FileName IS NULL)
		AND
		([Type] LIKE @Type OR @Type IS NULL)
		AND
		([ModuleID] = @ModuleID OR @ModuleID IS NULL)
		AND
		([ControllerActiveObjectType] LIKE @ControllerActiveObjectType OR @ControllerActiveObjectType IS NULL)
		AND
		([IsContent] = @IsContent OR @IsContent IS NULL)
		AND
		([IsContentContainer] = @IsContentContainer OR @IsContentContainer IS NULL)


    CREATE UNIQUE CLUSTERED INDEX
        idx_uc_rownum
    ON
        #results(RowNumberColumn)

    
    SELECT @RowIndex_total = COUNT(RowNumberColumn) FROM #results

    IF @RowIndex_end <= 0
        SET @RowIndex_end = @RowIndex_total + 1

    IF @RowIndex_end IS NULL OR @RowIndex_end < @RowIndex_start
        SET @RowIndex_end = @RowIndex_start + 1000    

    SELECT * FROM ModuleControls WHERE ID IN
    (SELECT ID FROM #results WHERE RowNumberColumn BETWEEN @RowIndex_start AND (@RowIndex_end - 1))
    ORDER BY
        ID

    DROP TABLE #results
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'85CFBC8C9FCC67B0D1D4380C62877156' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'modulecontrols_List'
GO
/****** Object:  StoredProcedure [dbo].[templates_Delete]    Script Date: 01/13/2008 23:21:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[templates_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[Templates]
	WHERE
		[ID]=@Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'42FCF18E7FD4ADDA8C7ACED7513BD9FD' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'templates_Delete'
GO
/****** Object:  StoredProcedure [dbo].[templates_Save]    Script Date: 01/13/2008 23:21:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[templates_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@Name [nvarchar](255) = null OUTPUT,
	@Path [nvarchar](255) = null OUTPUT,
	@UpdatedAt [datetime] = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM Templates WHERE [ID] = @Id)
		INSERT INTO Templates([ID], [Name], [Path], [UpdatedAt]) 
        VALUES (@Id, @Name, @Path, @UpdatedAt)
	ELSE
		UPDATE
			Templates
		SET
			[ID] = @Id,
			[Name] = @Name,
			[Path] = @Path,
			[UpdatedAt] = @UpdatedAt
		WHERE
			[ID] = @Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'4E4493601487ECDC50B0E63E7E67F6B5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'templates_Save'
GO
/****** Object:  StoredProcedure [dbo].[templates_List]    Script Date: 01/13/2008 23:21:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[templates_List]
	@Id [uniqueidentifier] = null,
	@Name [nvarchar](255) = null,
	@Path [nvarchar](255) = null,
	@UpdatedAt [datetime] = null
AS

	SELECT
		*
	FROM
		Templates
	WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		([Name] LIKE @Name OR @Name IS NULL)
		AND
		([Path] LIKE @Path OR @Path IS NULL)
		AND
		([UpdatedAt] = @UpdatedAt OR @UpdatedAt IS NULL)
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'DA8DFD4D754143242302DDAC180333ED' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'templates_List'
GO
/****** Object:  StoredProcedure [dbo].[routes_Delete]    Script Date: 01/13/2008 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[routes_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[Routes]
	WHERE
		[ID]=@Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'A7AF2F092D0DE0B4F5177CFBF79EBAC7' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'routes_Delete'
GO
/****** Object:  StoredProcedure [dbo].[routes_Save]    Script Date: 01/13/2008 23:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[routes_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@Pattern [nvarchar](max) = null OUTPUT,
	@ModuleControlID [uniqueidentifier] = null OUTPUT,
	@Order [int] = null OUTPUT,
	@IsDefault [bit] = null OUTPUT,
	@Name [nvarchar](255) = null OUTPUT,
	@FormatString [nvarchar](255) = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM Routes WHERE [ID] = @Id)
		INSERT INTO Routes([ID], [Pattern], [ModuleControlID], [Order], [IsDefault], [Name], [FormatString]) 
        VALUES (@Id, @Pattern, @ModuleControlID, @Order, @IsDefault, @Name, @FormatString)
	ELSE
		UPDATE
			Routes
		SET
			[ID] = @Id,
			[Pattern] = @Pattern,
			[ModuleControlID] = @ModuleControlID,
			[Order] = @Order,
			[IsDefault] = @IsDefault,
			[Name] = @Name,
			[FormatString] = @FormatString
		WHERE
			[ID] = @Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'FF774399E9916D2C104D7F67F7421BC0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'routes_Save'
GO
/****** Object:  StoredProcedure [dbo].[routes_List]    Script Date: 01/13/2008 23:21:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[routes_List]
	@Id [uniqueidentifier] = null,
	@Pattern [nvarchar](max) = null,
	@ModuleControlID [uniqueidentifier] = null,
	@Order [int] = null,
	@IsDefault [bit] = null,
	@Name [nvarchar](255) = null,
	@FormatString [nvarchar](255) = null,
	@RowIndex_start [bigint] = null,
	@RowIndex_end [bigint] = null,
	@RowIndex_total [bigint] = null OUTPUT
AS

    IF @RowIndex_start IS NULL OR @RowIndex_start < 0
        SET @RowIndex_start = 0

    SELECT
        ROW_NUMBER() OVER(ORDER BY [Order]) as RowNumberColumn, ID
    INTO
        #results
	FROM
		dbo.Routes
	WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		([Pattern] LIKE @Pattern OR @Pattern IS NULL)
		AND
		([ModuleControlID] = @ModuleControlID OR @ModuleControlID IS NULL)
		AND
		([Order] = @Order OR @Order IS NULL)
		AND
		([IsDefault] = @IsDefault OR @IsDefault IS NULL)
		AND
		([Name] LIKE @Name OR @Name IS NULL)
		AND
		([FormatString] LIKE @FormatString OR @FormatString IS NULL)


    CREATE UNIQUE CLUSTERED INDEX
        idx_uc_rownum
    ON
        #results(RowNumberColumn)

    
    SELECT @RowIndex_total = COUNT(RowNumberColumn) FROM #results

    IF @RowIndex_end <= 0
        SET @RowIndex_end = @RowIndex_total + 1

    IF @RowIndex_end IS NULL OR @RowIndex_end < @RowIndex_start
        SET @RowIndex_end = @RowIndex_start + 1000    

    SELECT * FROM Routes WHERE ID IN
    (SELECT ID FROM #results WHERE RowNumberColumn BETWEEN @RowIndex_start AND (@RowIndex_end - 1))
    ORDER BY
        [Order]

    DROP TABLE #results
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'3D5E841644997A1134AC5F30150BAF40' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'routes_List'
GO
/****** Object:  StoredProcedure [dbo].[permissionflags_Delete]    Script Date: 01/13/2008 23:21:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[permissionflags_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[PermissionFlags]
	WHERE
		[ID]=@Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'0AB0D136ED3B809C317B19B20A92E005' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'permissionflags_Delete'
GO
/****** Object:  StoredProcedure [dbo].[permissionflags_Save]    Script Date: 01/13/2008 23:21:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[permissionflags_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@Name [nvarchar](255) = null OUTPUT,
	@Flag [nvarchar](255) = null OUTPUT,
	@ModuleID [uniqueidentifier] = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM PermissionFlags WHERE [ID] = @Id)
		INSERT INTO PermissionFlags([ID], [Name], [Flag], [ModuleID]) 
        VALUES (@Id, @Name, @Flag, @ModuleID)
	ELSE
		UPDATE
			PermissionFlags
		SET
			[ID] = @Id,
			[Name] = @Name,
			[Flag] = @Flag,
			[ModuleID] = @ModuleID
		WHERE
			[ID] = @Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'B36F93E6475E70FF6C6E9D7093267671' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'permissionflags_Save'
GO
/****** Object:  StoredProcedure [dbo].[permissionflags_List]    Script Date: 01/13/2008 23:21:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[permissionflags_List]
	@Id [uniqueidentifier] = null,
	@Name [nvarchar](255) = null,
	@Flag [nvarchar](255) = null,
	@ModuleID [uniqueidentifier] = null
AS

	SELECT
		[ID],
		[Name],
		[Flag],
		[ModuleID]
	FROM
		dbo.PermissionFlags
	WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		([Name] LIKE @Name OR @Name IS NULL)
		AND
		([Flag] LIKE @Flag OR @Flag IS NULL)
		AND
		([ModuleID] = @ModuleID OR @ModuleID IS NULL)
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'DA99E5C331A83B8C623300F50F1EF66E' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'permissionflags_List'
GO
/****** Object:  StoredProcedure [dbo].[rssfeeds_Delete]    Script Date: 01/13/2008 23:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rssfeeds_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[RssFeeds]
	WHERE
		[ID]=@Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'1B375EA9848082AAECD76605618E830B' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'rssfeeds_Delete'
GO
/****** Object:  StoredProcedure [dbo].[rssfeeds_Save]    Script Date: 01/13/2008 23:21:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rssfeeds_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@Title [nvarchar](255) = null OUTPUT,
	@Link [nvarchar](255) = null OUTPUT,
	@Description [nvarchar](max) = null OUTPUT,
	@ManagingEditor [nvarchar](255) = null OUTPUT,
	@ItemFormat [nvarchar](max) = null OUTPUT,
	@Slug [nvarchar](50) = null OUTPUT,
	@RedirectUrl [nvarchar](255) = null OUTPUT,
	@RedirectExceptions [nvarchar](max) = null OUTPUT,
	@ControllerID [uniqueidentifier] = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM RssFeeds WHERE [ID] = @Id)
		INSERT INTO RssFeeds([ID], [Title], [Link], [Description], [ManagingEditor], [ItemFormat], [Slug], [RedirectUrl], [RedirectExceptions], [ControllerID]) 
        VALUES (@Id, @Title, @Link, @Description, @ManagingEditor, @ItemFormat, @Slug, @RedirectUrl, @RedirectExceptions, @ControllerID)
	ELSE
		UPDATE
			RssFeeds
		SET
			[ID] = @Id,
			[Title] = @Title,
			[Link] = @Link,
			[Description] = @Description,
			[ManagingEditor] = @ManagingEditor,
			[ItemFormat] = @ItemFormat,
			[Slug] = @Slug,
			[RedirectUrl] = @RedirectUrl,
			[RedirectExceptions] = @RedirectExceptions,
			[ControllerID] = @ControllerID
		WHERE
			[ID] = @Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'AC3E874007546CB5EDD6F807F5375AC8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'rssfeeds_Save'
GO
/****** Object:  StoredProcedure [dbo].[rssfeeds_List]    Script Date: 01/13/2008 23:21:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rssfeeds_List]
	@Id [uniqueidentifier] = null,
	@Title [nvarchar](255) = null,
	@Link [nvarchar](255) = null,
	@Description [nvarchar](max) = null,
	@ManagingEditor [nvarchar](255) = null,
	@ItemFormat [nvarchar](max) = null,
	@Slug [nvarchar](50) = null,
	@RedirectUrl [nvarchar](255) = null,
	@RedirectExceptions [nvarchar](max) = null,
	@ControllerID [uniqueidentifier] = null,
	@RowIndex_start [bigint] = null,
	@RowIndex_end [bigint] = null,
	@RowIndex_total [bigint] = null OUTPUT
AS

    IF @RowIndex_start IS NULL OR @RowIndex_start < 0
        SET @RowIndex_start = 0

    SELECT
        ROW_NUMBER() OVER(ORDER BY ID) as RowNumberColumn, ID
    INTO
        #results
	FROM
		dbo.RssFeeds
	WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		([Title] LIKE @Title OR @Title IS NULL)
		AND
		([Link] LIKE @Link OR @Link IS NULL)
		AND
		([Description] LIKE @Description OR @Description IS NULL)
		AND
		([ManagingEditor] LIKE @ManagingEditor OR @ManagingEditor IS NULL)
		AND
		([ItemFormat] LIKE @ItemFormat OR @ItemFormat IS NULL)
		AND
		([Slug] LIKE @Slug OR @Slug IS NULL)
		AND
		([RedirectUrl] LIKE @RedirectUrl OR @RedirectUrl IS NULL)
		AND
		([RedirectExceptions] LIKE @RedirectExceptions OR @RedirectExceptions IS NULL)
		AND
		([ControllerID] = @ControllerID OR @ControllerID IS NULL)


    CREATE UNIQUE CLUSTERED INDEX
        idx_uc_rownum
    ON
        #results(RowNumberColumn)

    
    SELECT @RowIndex_total = COUNT(RowNumberColumn) FROM #results

    IF @RowIndex_end <= 0
        SET @RowIndex_end = @RowIndex_total + 1

    IF @RowIndex_end IS NULL OR @RowIndex_end < @RowIndex_start
        SET @RowIndex_end = @RowIndex_start + 1000    

    SELECT * FROM RssFeeds WHERE ID IN
    (SELECT ID FROM #results WHERE RowNumberColumn BETWEEN @RowIndex_start AND (@RowIndex_end - 1))
    ORDER BY
        ID

    DROP TABLE #results
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'05466FDD23457812CB675B63C2D2A77A' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'rssfeeds_List'
GO
/****** Object:  StoredProcedure [dbo].[authors_Delete]    Script Date: 01/13/2008 23:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[authors_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[Authors]
	WHERE
		[ID]=@Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'BF4B8A0227354813585940FE19B8F2DC' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'authors_Delete'
GO
/****** Object:  StoredProcedure [dbo].[authors_Save]    Script Date: 01/13/2008 23:21:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[authors_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@UserName [nvarchar](255) = null OUTPUT,
	@Email [nvarchar](255) = null OUTPUT,
	@DisplayName [nvarchar](255) = null OUTPUT,
	@Bio [nvarchar](max) = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM Authors WHERE [ID] = @Id)
		INSERT INTO Authors([ID], [UserName], [Email], [DisplayName], [Bio]) 
        VALUES (@Id, @UserName, @Email, @DisplayName, @Bio)
	ELSE
		UPDATE
			Authors
		SET
			[ID] = @Id,
			[UserName] = @UserName,
			[Email] = @Email,
			[DisplayName] = @DisplayName,
			[Bio] = @Bio
		WHERE
			[ID] = @Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'B43624D656F1127085FBBAF15782D61A' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'authors_Save'
GO
/****** Object:  StoredProcedure [dbo].[authors_List]    Script Date: 01/13/2008 23:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[authors_List]
	@Id [uniqueidentifier] = null,
	@UserName [nvarchar](255) = null,
	@Email [nvarchar](255) = null,
	@DisplayName [nvarchar](255) = null,
	@Bio [nvarchar](max) = null,
	@RowIndex_start [bigint] = null,
	@RowIndex_end [bigint] = null,
	@RowIndex_total [bigint] = null OUTPUT
AS

    IF @RowIndex_start IS NULL OR @RowIndex_start < 0
        SET @RowIndex_start = 0

    SELECT
        ROW_NUMBER() OVER(ORDER BY ID) as RowNumberColumn, ID
    INTO
        #results
	FROM
		dbo.Authors
	WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		([UserName] LIKE @UserName OR @UserName IS NULL)
		AND
		([Email] LIKE @Email OR @Email IS NULL)
		AND
		([DisplayName] LIKE @DisplayName OR @DisplayName IS NULL)
		AND
		([Bio] LIKE @Bio OR @Bio IS NULL)


    CREATE UNIQUE CLUSTERED INDEX
        idx_uc_rownum
    ON
        #results(RowNumberColumn)


    IF @RowIndex_end IS NULL OR @RowIndex_end < @RowIndex_start
        SET @RowIndex_end = @RowIndex_start + 1000    
    
    SELECT @RowIndex_total = COUNT(RowNumberColumn) FROM #results

    SELECT * FROM Authors WHERE ID IN
    (SELECT ID FROM #results WHERE RowNumberColumn BETWEEN @RowIndex_start AND (@RowIndex_end - 1))
    ORDER BY
        ID

    DROP TABLE #results
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'A5523F636A734FBC5C6231B9914E712D' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'authors_List'
GO
