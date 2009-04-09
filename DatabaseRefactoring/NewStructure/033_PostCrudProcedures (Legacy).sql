CREATE PROCEDURE [dbo].[posts_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[v_LegacyPosts]
	WHERE
		[ID]=@Id
GO
Create PROCEDURE [dbo].[posts_List]
	@Id [uniqueidentifier] = null,
	@ID_list [xml] = null,
	@Slug [nvarchar](255) = null,
	@UserName [nvarchar](255) = null,
	@Title [nvarchar](255) = null,
	@Excerpt [nvarchar](max) = null,
	@Body [nvarchar](max) = null,
	@PublishDate [datetime] = null,
	@StartPublishDate [datetime] = null,
	@EndPublishDate [datetime] = null,
	@Status [int] = null,
	@DiscussionUrl [nvarchar](255) = null,
	@MoreUrl [nvarchar](255) = null,
	@ControllerID [uniqueidentifier] = null,
	@RowIndex_start [bigint] = null,
	@RowIndex_end [bigint] = null,
	@RowIndex_total [bigint] = null OUTPUT
AS

	IF (@ID IS NOT NULL OR (@Slug IS NOT NULL AND @ControllerID IS NOT NULL))
		BEGIN
			SET @RowIndex_total = 1
			SELECT * FROM v_LegacyPosts WHERE (ID = @ID OR (Slug = @Slug AND ControllerID = @ControllerID))
			AND (Status = @Status OR @Status is NULL)
			AND ([PublishDate] = @PublishDate OR @PublishDate IS NULL)
			AND ([PublishDate] BETWEEN @StartPublishDate AND @EndPublishDate OR (@StartPublishDate IS NULL AND @EndPublishDate IS NULL))
			RETURN
		END
    IF @RowIndex_start IS NULL OR @RowIndex_start < 0
        SET @RowIndex_start = 0

    IF @RowIndex_end IS NULL OR @RowIndex_end < @RowIndex_start
        SET @RowIndex_end = @RowIndex_start + 100
    
    
    DECLARE @ID_collection TABLE (ID uniqueidentifier)
    IF (@ID_list IS NOT NULL)
    BEGIN
        INSERT INTO @ID_collection (ID) SELECT ParamValues.ID.value('.','uniqueidentifier')as ID FROM @ID_list.nodes('/ID_list/ID') as ParamValues(ID)
    END

    SELECT 
        @RowIndex_total=COUNT(*) 
    FROM 
        v_LegacyPosts 
    WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		(@ID_list IS NULL OR ID in( select ID from @ID_collection ))		AND
		([Slug] LIKE @Slug OR @Slug IS NULL)
		AND
		([UserName] LIKE @UserName OR @UserName IS NULL)
		AND
		([Title] LIKE @Title OR @Title IS NULL)
		AND
		([Excerpt] LIKE @Excerpt OR @Excerpt IS NULL)
		AND
		([Body] LIKE @Body OR @Body IS NULL)
		AND
		([PublishDate] = @PublishDate OR @PublishDate IS NULL)
		AND
		([PublishDate] BETWEEN @StartPublishDate AND @EndPublishDate OR (@StartPublishDate IS NULL AND @EndPublishDate IS NULL))
		AND
		([Status] = @Status OR @Status IS NULL)
		AND
		([ControllerID] = @ControllerID OR @ControllerID IS NULL)
;

    WITH OrderedResults AS
    (SELECT *,
    ROW_NUMBER() OVER (order by PublishDate DESC) as _activeobjects_RowNumber
    FROM 
        dbo.v_LegacyPosts
    WHERE 
		([ID] = @Id OR @Id IS NULL)
		AND
		(@ID_list IS NULL OR ID in( select ID from @ID_collection ))		AND
		([Slug] LIKE @Slug OR @Slug IS NULL)
		AND
		([UserName] LIKE @UserName OR @UserName IS NULL)
		AND
		([Title] LIKE @Title OR @Title IS NULL)
		AND
		([Excerpt] LIKE @Excerpt OR @Excerpt IS NULL)
		AND
		([Body] LIKE @Body OR @Body IS NULL)
		AND
		([PublishDate] = @PublishDate OR @PublishDate IS NULL)
		AND
		([PublishDate] BETWEEN @StartPublishDate AND @EndPublishDate OR (@StartPublishDate IS NULL AND @EndPublishDate IS NULL))
		AND
		([Status] = @Status OR @Status IS NULL)
		AND
		([ControllerID] = @ControllerID OR @ControllerID IS NULL)

    ) 
    SELECT * 
    FROM OrderedResults
    WHERE _activeobjects_RowNumber BETWEEN @RowIndex_start AND (@RowIndex_end - 1);
GO
CREATE PROCEDURE [dbo].[posts_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@Slug [nvarchar](255) = null OUTPUT,
	@UserName [nvarchar](255) = null OUTPUT,
	@Title [nvarchar](255) = null OUTPUT,
	@Excerpt [nvarchar](max) = null OUTPUT,
	@Body [nvarchar](max) = null OUTPUT,
	@PublishDate [datetime] = null OUTPUT,
	@Status [int] = null OUTPUT,
	@DiscussionUrl [nvarchar](255) = null OUTPUT,
	@MoreUrl [nvarchar](255) = null OUTPUT,
	@ControllerID [uniqueidentifier] = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM v_LegacyPosts WHERE [ID] = @Id)
		INSERT INTO Posts([ID], [Slug], [UserName], [Title], [Excerpt], [Body], [PublishDate], [Status], [DiscussionUrl], [MoreUrl], [ControllerID]) 
        VALUES (@Id, @Slug, @UserName, @Title, @Excerpt, @Body, @PublishDate, @Status, @DiscussionUrl, @MoreUrl, @ControllerID)
	ELSE
		UPDATE
			v_LegacyPosts
		SET
			[ID] = @Id,
			[Slug] = @Slug,
			[UserName] = @UserName,
			[Title] = @Title,
			[Excerpt] = @Excerpt,
			[Body] = @Body,
			[PublishDate] = @PublishDate,
			[Status] = @Status,
			[ControllerID] = @ControllerID
		WHERE
			[ID] = @Id
GO
CREATE PROCEDURE [dbo].[posts_GetNext]
	@ID [uniqueidentifier]
AS

            DECLARE @ControllerID uniqueidentifier
            DECLARE @PostDate datetime

            SELECT @ControllerID=ControllerID, @PostDate=PublishDate FROM Posts WHERE ID=@ID

            IF(@ControllerID IS NOT NULL AND @PostDate IS NOT NULL)

            SELECT TOP 1 * FROM v_LegacyPosts WHERE PublishDate > @PostDate AND PublishDate < getDate() AND ControllerID=@ControllerID AND Status=1 ORDER BY PublishDate

GO

CREATE PROCEDURE [dbo].[posts_GetPrevious]
	@ID [uniqueidentifier]
AS

            DECLARE @ControllerID uniqueidentifier
            DECLARE @PostDate datetime

            SELECT @ControllerID=ControllerID, @PostDate=PublishDate FROM Posts WHERE ID=@ID

            IF(@ControllerID IS NOT NULL AND @PostDate IS NOT NULL)

            SELECT TOP 1 * FROM Posts WHERE PublishDate < @PostDate AND ControllerID=@ControllerID AND Status=1 ORDER BY PublishDate DESC

GO
