/****** Object:  StoredProcedure [dbo].[discussions_Delete]    Script Date: 01/15/2008 14:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[discussions_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[Discussions]
	WHERE
		[ID]=@Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'F99250D4B96C7804FB1519DB844B0397' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'discussions_Delete'
GO
/****** Object:  StoredProcedure [dbo].[discussions_Save]    Script Date: 01/15/2008 14:02:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[discussions_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@ActiveObjectType [nvarchar](255) = null OUTPUT,
	@ActiveObjectID [uniqueidentifier] = null OUTPUT,
	@DiscussionLink [nvarchar](255) = null OUTPUT,
	@Status [int] = null OUTPUT,
	@LastStatusMessage [nvarchar](255) = null OUTPUT,
	@DiscussionProviderID [uniqueidentifier] = null OUTPUT,
	@PublishDate [datetime] = null OUTPUT,
	@CommentCount [int] = null OUTPUT,
	@Title [nvarchar](255) = null OUTPUT,
	@Excerpt [nvarchar](max) = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM Discussions WHERE [ID] = @Id)
		INSERT INTO Discussions([ID], [ContentItemID], [DiscussionLink], [Status], [LastStatusMessage], [DiscussionProviderID], [CommentCount]) 
        VALUES (@Id, @ActiveObjectID, @DiscussionLink, @Status, @LastStatusMessage, @DiscussionProviderID, @CommentCount)
	ELSE
		UPDATE
			Discussions
		SET
			[ID] = @Id,
			--[ActiveObjectType] = @ActiveObjectType,
			[ContentItemID] = @ActiveObjectID,
			[DiscussionLink] = @DiscussionLink,
			[Status] = @Status,
			[LastStatusMessage] = @LastStatusMessage,
			[DiscussionProviderID] = @DiscussionProviderID,
			[CommentCount] = @CommentCount
		WHERE
			[ID] = @Id
GO
/****** Object:  StoredProcedure [dbo].[discussions_List]    Script Date: 01/15/2008 14:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[discussions_List]
	@Id [uniqueidentifier] = null,
	@ID_list [xml] = null,
	@ActiveObjectType [nvarchar](255) = null,
	@ActiveObjectID [uniqueidentifier] = null,
	@DiscussionLink [nvarchar](255) = null,
	@Status [int] = null,
	@LastStatusMessage [nvarchar](255) = null,
	@DiscussionProviderID [uniqueidentifier] = null,
	@PublishDate [datetime] = null,
	@CommentCount [int] = null,
	@Title [nvarchar](255) = null,
	@Excerpt [nvarchar](max) = null,
	@RowIndex_start [bigint] = null,
	@RowIndex_end [bigint] = null,
	@RowIndex_total [bigint] = null OUTPUT
AS

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
        Discussions 
    WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		(@ID_list IS NULL OR ID in( select ID from @ID_collection ))		AND
		([DiscussionLink] LIKE @DiscussionLink OR @DiscussionLink IS NULL)
		AND
		([Status] = @Status OR @Status IS NULL)
		AND
		([LastStatusMessage] LIKE @LastStatusMessage OR @LastStatusMessage IS NULL)
		AND
		([DiscussionProviderID] = @DiscussionProviderID OR @DiscussionProviderID IS NULL)
		AND
		([CommentCount] = @CommentCount OR @CommentCount IS NULL)
;

    WITH OrderedResults AS
    (SELECT *,
    ROW_NUMBER() OVER (order by ID) as _activeobjects_RowNumber
    FROM 
        dbo.Discussions
    WHERE 
		([ID] = @Id OR @Id IS NULL)
		AND
		(@ID_list IS NULL OR ID in( select ID from @ID_collection ))		AND
		([ContentItemID] = @ActiveObjectID OR @ActiveObjectID IS NULL)
		AND
		([DiscussionLink] LIKE @DiscussionLink OR @DiscussionLink IS NULL)
		AND
		([Status] = @Status OR @Status IS NULL)
		AND
		([LastStatusMessage] LIKE @LastStatusMessage OR @LastStatusMessage IS NULL)
		AND
		([DiscussionProviderID] = @DiscussionProviderID OR @DiscussionProviderID IS NULL)
		AND
		([CommentCount] = @CommentCount OR @CommentCount IS NULL)

    ) 
    SELECT a.ID, a.ContentItemID AS ActiveObjectID, a.DiscussionLink, a.Status, a.LastStatusMessage,
	a.DiscussionProviderID, b.PublishDate, a.CommentCount, c.Title, c.Excerpt
    FROM OrderedResults a, ContentItems b, TextBlocks c
    WHERE
		a.ContentItemID = b.ID and b.TextBlockID = c.ID AND
		_activeobjects_RowNumber BETWEEN @RowIndex_start AND (@RowIndex_end - 1);
GO
/****** Object:  StoredProcedure [dbo].[permissions_Delete]    Script Date: 01/15/2008 14:02:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[permissions_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[Permissions]
	WHERE
		[ID]=@Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'96C6BAE4D401227F99187C74BAC0C68B' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'permissions_Delete'
GO
/****** Object:  StoredProcedure [dbo].[permissions_Save]    Script Date: 01/15/2008 14:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[permissions_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@Type [nvarchar](255) = null OUTPUT,
	@ActiveObjectID [uniqueidentifier] = null OUTPUT,
	@Group [nvarchar](255) = null OUTPUT,
	@Flag [nvarchar](255) = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM Permissions WHERE [ID] = @Id)
		INSERT INTO Permissions([ID], [ControllerID], [Group], [Flag]) 
        VALUES (@Id, @ActiveObjectID, @Group, @Flag)
	ELSE
		UPDATE
			Permissions
		SET
			[ID] = @Id,
			[ControllerID] = @ActiveObjectID,
			[Group] = @Group,
			[Flag] = @Flag
		WHERE
			[ID] = @Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'20EEB08BA6ECD135681C397EC2A5387C' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'permissions_Save'
GO
/****** Object:  StoredProcedure [dbo].[permissions_List]    Script Date: 01/15/2008 14:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[permissions_List]
	@Id [uniqueidentifier] = null,
	@ID_list [xml] = null,
	@Type [nvarchar](255) = null,
	@ActiveObjectID [uniqueidentifier] = null,
	@Group [nvarchar](255) = null,
	@Flag [nvarchar](255) = null,
	@RowIndex_start [bigint] = null,
	@RowIndex_end [bigint] = null,
	@RowIndex_total [bigint] = null OUTPUT
AS

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
        Permissions 
    WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		(@ID_list IS NULL OR ID in( select ID from @ID_collection ))		
		AND
		([ControllerID] = @ActiveObjectID OR @ActiveObjectID IS NULL)
		AND
		([Group] LIKE @Group OR @Group IS NULL)
		AND
		([Flag] LIKE @Flag OR @Flag IS NULL)
;

    WITH OrderedResults AS
    (SELECT *,
    ROW_NUMBER() OVER (order by ID) as _activeobjects_RowNumber
    FROM 
        dbo.Permissions
    WHERE 
		([ID] = @Id OR @Id IS NULL)
		AND
		(@ID_list IS NULL OR ID in( select ID from @ID_collection ))		AND

		([ControllerID] = @ActiveObjectID OR @ActiveObjectID IS NULL)
		AND
		([Group] LIKE @Group OR @Group IS NULL)
		AND
		([Flag] LIKE @Flag OR @Flag IS NULL)

    ) 
    SELECT ID, ControllerID as ActiveObjectID, [Group], Flag, '' as Type 
    FROM OrderedResults
    WHERE _activeobjects_RowNumber BETWEEN @RowIndex_start AND (@RowIndex_end - 1);
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'19DFCD81AFF934D2FC13691B4B71BF89' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'permissions_List'
GO
/****** Object:  StoredProcedure [dbo].[pages_Delete]    Script Date: 01/15/2008 14:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pages_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[Pages]
	WHERE
		[ID]=@Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'9A69B1FB232A332A7B2A36B2806050BB' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'pages_Delete'
GO
/****** Object:  StoredProcedure [dbo].[pages_Save]    Script Date: 01/15/2008 14:02:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pages_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@PageNumber [int] = null OUTPUT,
	@Name [nvarchar](255) = null OUTPUT,
	@Body [nvarchar](max) = null OUTPUT,
	@ControllerID [uniqueidentifier] = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM Pages WHERE [ID] = @Id)
		BEGIN
		INSERT INTO TextBlocks(ID, Title, Body) VALUES(@ID, @Name, @Body)
		INSERT INTO Pages([ID], [PageNumber], TextBlockID, [ContentItemID]) 
        VALUES (@Id, @PageNumber, @ID, @ControllerID)
		END
	ELSE
		BEGIN
		UPDATE
			Pages
		SET
			[ID] = @Id,
			[PageNumber] = @PageNumber,
			[ContentItemID] = @ControllerID
		WHERE
			[ID] = @Id
		UPDATE TextBlocks SET Title = @Name, Body = @Body WHERE ID IN 
			(SELECT TextBlockID from Pages WHERE ID = @ID)
		END
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'CEDD3DF54A4B6DA7E28E1455AAA20793' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'pages_Save'
GO
/****** Object:  StoredProcedure [dbo].[pages_List]    Script Date: 01/15/2008 14:02:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pages_List]
	@Id [uniqueidentifier] = null,
	@ID_list [xml] = null,
	@PageNumber [int] = null,
	@Name [nvarchar](255) = null,
	@Body [nvarchar](max) = null,
	@ControllerID [uniqueidentifier] = null,
	@RowIndex_start [bigint] = null,
	@RowIndex_end [bigint] = null,
	@RowIndex_total [bigint] = null OUTPUT
AS

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
        Pages 
    WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		(@ID_list IS NULL OR ID in( select ID from @ID_collection ))		AND
		([PageNumber] = @PageNumber OR @PageNumber IS NULL)
		AND
		([ContentItemID] = @ControllerID OR @ControllerID IS NULL)
;

    WITH OrderedResults AS
    (SELECT *,
    ROW_NUMBER() OVER (order by PageNumber) as _activeobjects_RowNumber
    FROM 
        dbo.Pages
    WHERE 
		([ID] = @Id OR @Id IS NULL)
		AND
		(@ID_list IS NULL OR ID in( select ID from @ID_collection ))		
		AND
		([PageNumber] = @PageNumber OR @PageNumber IS NULL)
		AND
		([ContentItemID] = @ControllerID OR @ControllerID IS NULL)

    ) 
    SELECT a.*, b.Title as Name, b.Body, a.ContentItemID as ControllerID
    FROM OrderedResults a, TextBlocks b
	WHERE a.TextBlockID = b.ID AND
	_activeobjects_RowNumber BETWEEN @RowIndex_start AND (@RowIndex_end - 1);
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'216F4746C0D9BEF726F9C81AEA62D054' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'pages_List'
GO
/****** Object:  StoredProcedure [dbo].[urlredirects_Delete]    Script Date: 01/15/2008 14:02:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[urlredirects_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[UrlRedirects]
	WHERE
		[ID]=@Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'8FAAA1D52436CD4AECE2A7A599DAA7E2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'urlredirects_Delete'
GO
/****** Object:  StoredProcedure [dbo].[urlredirects_Save]    Script Date: 01/15/2008 14:02:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[urlredirects_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@Url [nvarchar](255) = null OUTPUT,
	@PathExtra [nvarchar](255) = null OUTPUT,
	@ControllerID [uniqueidentifier] = null OUTPUT,
	@Handler [nvarchar](255) = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM UrlRedirects WHERE [ID] = @Id)
		INSERT INTO UrlRedirects([ID], [Url], [PathExtra], [ContentItemID], [Handler]) 
        VALUES (@Id, @Url, @PathExtra, @ControllerID, @Handler)
	ELSE
		UPDATE
			UrlRedirects
		SET
			[ID] = @Id,
			[Url] = @Url,
			[PathExtra] = @PathExtra,
			[ContentItemID] = @ControllerID,
			[Handler] = @Handler
		WHERE
			[ID] = @Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'1F4ACBE4BC2B3BAD4AA4D25803E6CF3D' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'urlredirects_Save'
GO
/****** Object:  StoredProcedure [dbo].[urlredirects_List]    Script Date: 01/15/2008 14:02:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[urlredirects_List]
	@Id [uniqueidentifier] = null,
	@ID_list [xml] = null,
	@Url [nvarchar](255) = null,
	@PathExtra [nvarchar](255) = null,
	@ControllerID [uniqueidentifier] = null,
	@Handler [nvarchar](255) = null,
	@RowIndex_start [bigint] = null,
	@RowIndex_end [bigint] = null,
	@RowIndex_total [bigint] = null OUTPUT
AS

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
        UrlRedirects 
    WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		(@ID_list IS NULL OR ID in( select ID from @ID_collection ))		AND
		([Url] LIKE @Url OR @Url IS NULL)
		AND
		([PathExtra] LIKE @PathExtra OR @PathExtra IS NULL)
		AND
		([ContentItemID] = @ControllerID OR @ControllerID IS NULL)
		AND
		([Handler] LIKE @Handler OR @Handler IS NULL)
;

    WITH OrderedResults AS
    (SELECT *,
    ROW_NUMBER() OVER (order by ID) as _activeobjects_RowNumber
    FROM 
        dbo.UrlRedirects
    WHERE 
		([ID] = @Id OR @Id IS NULL)
		AND
		(@ID_list IS NULL OR ID in( select ID from @ID_collection ))		AND
		([Url] LIKE @Url OR @Url IS NULL)
		AND
		([PathExtra] LIKE @PathExtra OR @PathExtra IS NULL)
		AND
		([ContentItemID] = @ControllerID OR @ControllerID IS NULL)
		AND
		([Handler] LIKE @Handler OR @Handler IS NULL)

    ) 
    SELECT *, ContentItemID as ControllerID 
    FROM OrderedResults
    WHERE _activeobjects_RowNumber BETWEEN @RowIndex_start AND (@RowIndex_end - 1);
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'D7C5477C46E9117DBEF241E7FE444358' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'urlredirects_List'
GO
/****** Object:  StoredProcedure [dbo].[scheduledcommands_Delete]    Script Date: 01/15/2008 14:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[scheduledcommands_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[ScheduledCommands]
	WHERE
		[ID]=@Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'948B36411485D2400799E2916ACC9833' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'scheduledcommands_Delete'
GO
/****** Object:  StoredProcedure [dbo].[scheduledcommands_Save]    Script Date: 01/15/2008 14:02:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[scheduledcommands_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@RunAt [datetime] = null OUTPUT,
	@Type [nvarchar](255) = null OUTPUT,
	@ActiveObjectID [uniqueidentifier] = null OUTPUT,
	@Command [nvarchar](max) = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()

	IF NOT EXISTS (SELECT [ID] FROM ScheduledCommands WHERE [ID] = @Id)
		INSERT INTO ScheduledCommands([ID], [RunAt], [ContentItemID], [Command]) 
        VALUES (@Id, @RunAt, @ActiveObjectID, @Command)
	ELSE
		UPDATE
			ScheduledCommands
		SET
			[ID] = @Id,
			[RunAt] = @RunAt,
			[ContentItemID] = @ActiveObjectID,
			[Command] = @Command
		WHERE
			[ID] = @Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'F38F1B922F4CFC388C2A0CA389B2F3BD' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'scheduledcommands_Save'
GO
/****** Object:  StoredProcedure [dbo].[scheduledcommands_List]    Script Date: 01/15/2008 14:02:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[scheduledcommands_List]
	@Id [uniqueidentifier] = null,
	@ID_list [xml] = null,
	@RunAt [datetime] = null,
	@StartRunAt [datetime] = null,
	@EndRunAt [datetime] = null,
	@Type [nvarchar](255) = null,
	@ActiveObjectID [uniqueidentifier] = null,
	@Command [nvarchar](max) = null,
	@RowIndex_start [bigint] = null,
	@RowIndex_end [bigint] = null,
	@RowIndex_total [bigint] = null OUTPUT
AS

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
        ScheduledCommands 
    WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		(@ID_list IS NULL OR ID in( select ID from @ID_collection ))		AND
		([RunAt] = @RunAt OR @RunAt IS NULL)
		AND
		([RunAt] BETWEEN @StartRunAt AND @EndRunAt OR (@StartRunAt IS NULL AND @EndRunAt IS NULL))
		AND
		([ContentItemID] = @ActiveObjectID OR @ActiveObjectID IS NULL)
		AND
		([Command] LIKE @Command OR @Command IS NULL)
;

    WITH OrderedResults AS
    (SELECT *,
    ROW_NUMBER() OVER (order by ID) as _activeobjects_RowNumber
    FROM 
        dbo.ScheduledCommands
    WHERE 
		([ID] = @Id OR @Id IS NULL)
		AND
		(@ID_list IS NULL OR ID in( select ID from @ID_collection ))		AND
		([RunAt] = @RunAt OR @RunAt IS NULL)
		AND
		([RunAt] BETWEEN @StartRunAt AND @EndRunAt OR (@StartRunAt IS NULL AND @EndRunAt IS NULL))
		AND
		([ContentItemID] = @ActiveObjectID OR @ActiveObjectID IS NULL)
		AND
		([Command] LIKE @Command OR @Command IS NULL)

    ) 
    SELECT a.*, c.ActiveObjectType as Type
    FROM OrderedResults a, ContentItems b, ContentTypes c
	WHERE a.ContentItemID = b.ID and b.ContentTypeID = c.ID AND
    _activeobjects_RowNumber BETWEEN @RowIndex_start AND (@RowIndex_end - 1);
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'DBBE9C32C0144B24366172609E47C08A' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'scheduledcommands_List'
GO
/****** Object:  StoredProcedure [dbo].[tags_Delete]    Script Date: 01/15/2008 14:02:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[tags_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[Tags]
	WHERE
		[ID]=@Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'3818F0C1C4699155E22C97D6B9E0FD3A' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'tags_Delete'
GO
/****** Object:  StoredProcedure [dbo].[tags_Save]    Script Date: 01/15/2008 14:02:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[tags_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@Type [nvarchar](255) = null OUTPUT,
	@Name [nvarchar](255) = null OUTPUT,
	@StringValue [nvarchar](255) = null OUTPUT,
	@StringValueNormalized [nvarchar](255) = null OUTPUT,
	@NumericValue [float] = null OUTPUT,
	@ActiveObjectID [uniqueidentifier] = null OUTPUT,
	@NormalizeStringValue [bit] = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM Tags WHERE [ID] = @Id)
		INSERT INTO Tags([ID], [Name], [StringValue], [StringValueNormalized], [NumericValue], [ContentItemID], [NormalizeStringValue]) 
        VALUES (@Id, @Name, @StringValue, @StringValueNormalized, @NumericValue, @ActiveObjectID, @NormalizeStringValue)
	ELSE
		UPDATE
			Tags
		SET
			[ID] = @Id,
			[Name] = @Name,
			[StringValue] = @StringValue,
			[StringValueNormalized] = @StringValueNormalized,
			[NumericValue] = @NumericValue,
			[ContentItemID] = @ActiveObjectID,
			[NormalizeStringValue] = @NormalizeStringValue
		WHERE
			[ID] = @Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'A98A29072B22A7F221C8A1CB4A2D2823' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'tags_Save'
GO
/****** Object:  StoredProcedure [dbo].[tags_List]    Script Date: 01/15/2008 14:02:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[tags_List]
	@Id [uniqueidentifier] = null,
	@ID_list [xml] = null,
	@Type [nvarchar](255) = null,
	@Name [nvarchar](255) = null,
	@StringValue [nvarchar](255) = null,
	@StringValueNormalized [nvarchar](255) = null,
	@NumericValue [float] = null,
	@ActiveObjectID [uniqueidentifier] = null,
	@NormalizeStringValue [bit] = null,
	@RowIndex_start [bigint] = null,
	@RowIndex_end [bigint] = null,
	@RowIndex_total [bigint] = null OUTPUT
AS

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
        Tags 
    WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		(@ID_list IS NULL OR ID in( select ID from @ID_collection ))		AND
		([Name] LIKE @Name OR @Name IS NULL)
		AND
		([StringValue] LIKE @StringValue OR @StringValue IS NULL)
		AND
		([StringValueNormalized] LIKE @StringValueNormalized OR @StringValueNormalized IS NULL)
		AND
		([NumericValue] = @NumericValue OR @NumericValue IS NULL)
		AND
		([ContentItemID] = @ActiveObjectID OR @ActiveObjectID IS NULL)
		AND
		([NormalizeStringValue] = @NormalizeStringValue OR @NormalizeStringValue IS NULL)
;

    WITH OrderedResults AS
    (SELECT *,
    ROW_NUMBER() OVER (order by ID) as _activeobjects_RowNumber
    FROM 
        dbo.Tags
    WHERE 
		([ID] = @Id OR @Id IS NULL)
		AND
		(@ID_list IS NULL OR ID in( select ID from @ID_collection ))		AND
		([Name] LIKE @Name OR @Name IS NULL)
		AND
		([StringValue] LIKE @StringValue OR @StringValue IS NULL)
		AND
		([StringValueNormalized] LIKE @StringValueNormalized OR @StringValueNormalized IS NULL)
		AND
		([NumericValue] = @NumericValue OR @NumericValue IS NULL)
		AND
		([ContentItemID] = @ActiveObjectID OR @ActiveObjectID IS NULL)
		AND
		([NormalizeStringValue] = @NormalizeStringValue OR @NormalizeStringValue IS NULL)

    ) 
    SELECT *, ContentItemID as ActiveObjectID 
    FROM OrderedResults
    WHERE _activeobjects_RowNumber BETWEEN @RowIndex_start AND (@RowIndex_end - 1);
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'DE8ED4D6AA3E3FBDBA0C648769781AE7' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'tags_List'
GO
/****** Object:  StoredProcedure [dbo].[tags_GetDistinctStringValues]    Script Date: 01/15/2008 14:02:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[tags_GetDistinctStringValues]
	@Name [nvarchar](255)
AS

select top 50 count(id) as NumericValue, Name, StringValue, StringValueNormalized from Tags where Name=@Name
group by name, stringvalue, stringvaluenormalized
order by count(id) desc
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'EDF088850C8458120AD9F9A2BCE6737F' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'tags_GetDistinctStringValues'
GO
/****** Object:  StoredProcedure [dbo].[files_Save]    Script Date: 01/15/2008 14:02:19 ******/
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[files_Save]
	@Id [uniqueidentifier] = null OUTPUT,
	@Name [nvarchar](255) = null OUTPUT,
	@FileName [nvarchar](255) = null OUTPUT,
	@PhysicalPath [nvarchar](255) = null OUTPUT,
	@MediaType [nvarchar](50) = null OUTPUT,
	@MediaSubType [nvarchar](50) = null OUTPUT,
	@UpdatedAt [datetime] = null OUTPUT,
	@ControllerID [uniqueidentifier] = null OUTPUT
AS

	IF @Id IS NULL OR @Id = Cast('{00000000-0000-0000-0000-000000000000}' as uniqueidentifier)
		SET @Id = newid()


	IF NOT EXISTS (SELECT [ID] FROM Files WHERE [ID] = @Id)
		INSERT INTO Files([ID], [Name], [FileName], [PhysicalPath], [MediaType], [MediaSubType], [ContentItemID]) 
        VALUES (@Id, @Name, @FileName, @PhysicalPath, @MediaType, @MediaSubType, @ControllerID)
	ELSE
		UPDATE
			Files
		SET
			[ID] = @Id,
			[Name] = @Name,
			[FileName] = @FileName,
			[PhysicalPath] = @PhysicalPath,
			[MediaType] = @MediaType,
			[MediaSubType] = @MediaSubType,
			[ContentItemID] = @ControllerID
		WHERE
			[ID] = @Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'F15F06F20A19E4BAC0ED75B51BA9CF30' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'files_Save'
GO
/****** Object:  StoredProcedure [dbo].[files_List]    Script Date: 01/15/2008 14:02:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[files_List]
	@Id [uniqueidentifier] = null,
	@ID_list [xml] = null,
	@Name [nvarchar](255) = null,
	@FileName [nvarchar](255) = null,
	@PhysicalPath [nvarchar](255) = null,
	@MediaType [nvarchar](50) = null,
	@MediaSubType [nvarchar](50) = null,
	@UpdatedAt [datetime] = null,
	@ControllerID [uniqueidentifier] = null,
	@RowIndex_start [bigint] = null,
	@RowIndex_end [bigint] = null,
	@RowIndex_total [bigint] = null OUTPUT
AS

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
        Files 
    WHERE
		([ID] = @Id OR @Id IS NULL)
		AND
		(@ID_list IS NULL OR ID in( select ID from @ID_collection ))		AND
		([Name] LIKE @Name OR @Name IS NULL)
		AND
		([FileName] LIKE @FileName OR @FileName IS NULL)
		AND
		([PhysicalPath] LIKE @PhysicalPath OR @PhysicalPath IS NULL)
		AND
		([MediaType] LIKE @MediaType OR @MediaType IS NULL)
		AND
		([MediaSubType] LIKE @MediaSubType OR @MediaSubType IS NULL)
		AND
		([ContentItemID] = @ControllerID OR @ControllerID IS NULL)
;

    WITH OrderedResults AS
    (SELECT *,
    ROW_NUMBER() OVER (order by FileName) as _activeobjects_RowNumber
    FROM 
        dbo.Files
    WHERE 
		([ID] = @Id OR @Id IS NULL)
		AND
		(@ID_list IS NULL OR ID in( select ID from @ID_collection ))		AND
		([Name] LIKE @Name OR @Name IS NULL)
		AND
		([FileName] LIKE @FileName OR @FileName IS NULL)
		AND
		([PhysicalPath] LIKE @PhysicalPath OR @PhysicalPath IS NULL)
		AND
		([MediaType] LIKE @MediaType OR @MediaType IS NULL)
		AND
		([MediaSubType] LIKE @MediaSubType OR @MediaSubType IS NULL)
		AND
		([ContentItemID] = @ControllerID OR @ControllerID IS NULL)

    ) 
    SELECT *, ContentItemID as ControllerID 
    FROM OrderedResults
    WHERE _activeobjects_RowNumber BETWEEN @RowIndex_start AND (@RowIndex_end - 1);
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'DF6865B307914ECCEA4256882F2E7F61' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'files_List'
GO
/****** Object:  StoredProcedure [dbo].[files_Delete]    Script Date: 01/15/2008 14:02:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[files_Delete]
	@Id [uniqueidentifier]
AS
	DELETE FROM
		[Files]
	WHERE
		[ID]=@Id
GO
EXEC sys.sp_addextendedproperty @name=N'ActiveObjectHash', @value=N'0AB9D3F93AE882A78CE22BC82D109B9B' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'files_Delete'
GO
