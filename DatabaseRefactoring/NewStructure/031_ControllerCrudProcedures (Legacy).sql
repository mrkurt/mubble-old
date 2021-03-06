/****** Object:  StoredProcedure [dbo].[controllers_Delete]    Script Date: 01/14/2008 00:41:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[controllers_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		v_LegacyControllers
	WHERE
		[ID]=@Id
GO
/****** Object:  StoredProcedure [dbo].[controllers_Save]    Script Date: 01/14/2008 00:41:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[controllers_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@Title [nvarchar](255) = null OUTPUT,
	@FileName [nvarchar](255) = null OUTPUT,
	@Body [nvarchar](max) = null OUTPUT,
	@PublishDate [datetime] = null OUTPUT,
	@ModifyDate [datetime] = null OUTPUT,
	@Settings [nvarchar](max) = null OUTPUT,
	@Status [int] = null OUTPUT,
	@UpdatedAt [datetime] = null OUTPUT,
	@TemplateID [uniqueidentifier] = null OUTPUT,
	@ControllerID [uniqueidentifier] = null OUTPUT,
	@TemplateControl [nvarchar](255) = null OUTPUT,
	@Excerpt [nvarchar](max) = null OUTPUT,
	@Summary [nvarchar](max) = null OUTPUT,
	@ActiveObjectType [nvarchar](255) = null OUTPUT,
	@IsContent [bit] = null OUTPUT,
	@IsContentContainer [bit] = null OUTPUT,
	@ModuleControlID [uniqueidentifier] = null OUTPUT,
	@ModuleControlView [nvarchar](255) = null OUTPUT,
	@RouteID [uniqueidentifier] = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM v_LegacyControllers WHERE [ID] = @Id)
		INSERT INTO v_LegacyControllers([ID], [Title], [FileName], [Body], [PublishDate], [Settings], [Status], [TemplateID], [ControllerID], [TemplateControl], [Excerpt], [ActiveObjectType], [IsContent], [IsContentContainer], [ModuleControlID], [ModuleControlView], [RouteID]) 
        VALUES (@Id, @Title, @FileName, @Body, @PublishDate, @Settings, @Status, @TemplateID, @ControllerID, @TemplateControl, @Excerpt, @ActiveObjectType, @IsContent, @IsContentContainer, @ModuleControlID, @ModuleControlView, @RouteID)
	ELSE
		UPDATE
			v_LegacyControllers
		SET
			[ID] = @Id,
			[Title] = @Title,
			[FileName] = @FileName,
			[Body] = @Body,
			[PublishDate] = @PublishDate,
			[Settings] = @Settings,
			[Status] = @Status,
			[TemplateID] = @TemplateID,
			[ControllerID] = @ControllerID,
			[TemplateControl] = @TemplateControl,
			[Excerpt] = @Excerpt,
			[ActiveObjectType] = @ActiveObjectType,
			[IsContent] = @IsContent,
			[IsContentContainer] = @IsContentContainer,
			[ModuleControlID] = @ModuleControlID,
			[ModuleControlView] = @ModuleControlView,
			[RouteID] = @RouteID
		WHERE
			[ID] = @Id
GO

/****** Object:  StoredProcedure [dbo].[controllers_List]    Script Date: 01/14/2008 00:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[controllers_List]
	@Id [uniqueidentifier] = null,
	@Title [nvarchar](255) = null,
	@FileName [nvarchar](255) = null,
	@Body [nvarchar](max) = null,
	@Path [nvarchar](255) = null,
	@PublishDate [datetime] = null,
	@ModifyDate [datetime] = null,
	@Settings [nvarchar](max) = null,
	@Status [int] = null,
	@UpdatedAt [datetime] = null,
	@TemplateID [uniqueidentifier] = null,
	@ControllerID [uniqueidentifier] = null,
	@TemplateControl [nvarchar](255) = null,
	@Excerpt [nvarchar](max) = null,
	@Summary [nvarchar](max) = null,
	@ActiveObjectType [nvarchar](255) = null,
	@IsContent [bit] = null,
	@IsContentContainer [bit] = null,
	@ModuleControlID [uniqueidentifier] = null,
	@ModuleControlView [nvarchar](255) = null,
	@RouteID [uniqueidentifier] = null,
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
		dbo.v_LegacyControllers
	WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		([Title] LIKE @Title OR @Title IS NULL)
		AND
		([FileName] LIKE @FileName OR @FileName IS NULL)
		AND
		([Body] LIKE @Body OR @Body IS NULL)
		AND
		([Path] LIKE @Path OR @Path IS NULL)
		AND
		([PublishDate] = @PublishDate OR @PublishDate IS NULL)
		AND
		([Settings] LIKE @Settings OR @Settings IS NULL)
		AND
		([Status] = @Status OR @Status IS NULL)
		AND
		([TemplateID] = @TemplateID OR @TemplateID IS NULL)
		AND
		([ControllerID] = @ControllerID OR @ControllerID IS NULL)
		AND
		([TemplateControl] LIKE @TemplateControl OR @TemplateControl IS NULL)
		AND
		([Excerpt] LIKE @Excerpt OR @Excerpt IS NULL)
		AND
		([ActiveObjectType] LIKE @ActiveObjectType OR @ActiveObjectType IS NULL)
		AND
		([IsContent] = @IsContent OR @IsContent IS NULL)
		AND
		([IsContentContainer] = @IsContentContainer OR @IsContentContainer IS NULL)
		AND
		([ModuleControlID] = @ModuleControlID OR @ModuleControlID IS NULL)
		AND
		([ModuleControlView] LIKE @ModuleControlView OR @ModuleControlView IS NULL)
		AND
		([RouteID] = @RouteID OR @RouteID IS NULL)


    CREATE UNIQUE CLUSTERED INDEX
        idx_uc_rownum
    ON
        #results(RowNumberColumn)

    
    SELECT @RowIndex_total = COUNT(RowNumberColumn) FROM #results

    IF @RowIndex_end <= 0
        SET @RowIndex_end = @RowIndex_total + 1

    IF @RowIndex_end IS NULL OR @RowIndex_end < @RowIndex_start
        SET @RowIndex_end = @RowIndex_start + 1000    

    SELECT * FROM v_LegacyControllers WHERE ID IN
    (SELECT ID FROM #results WHERE RowNumberColumn BETWEEN @RowIndex_start AND (@RowIndex_end - 1))
    ORDER BY
        ID

    DROP TABLE #results
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'D1E17CAA88CEFE92920CC9BEED0458E3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'controllers_List'
GO
