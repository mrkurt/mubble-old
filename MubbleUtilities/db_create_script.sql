IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Content_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
ALTER TABLE [dbo].[Content] DROP CONSTRAINT [FK_Content_Content]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Content_Modules]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
ALTER TABLE [dbo].[Content] DROP CONSTRAINT [FK_Content_Modules]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
ALTER TABLE [dbo].[Content] DROP CONSTRAINT [FK_Template_Content]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContentRoles_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContentRoles]'))
ALTER TABLE [dbo].[ContentRoles] DROP CONSTRAINT [FK_ContentRoles_Content]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContentRoles_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContentRoles]'))
ALTER TABLE [dbo].[ContentRoles] DROP CONSTRAINT [FK_ContentRoles_Roles]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContentUsers_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContentUsers]'))
ALTER TABLE [dbo].[ContentUsers] DROP CONSTRAINT [FK_ContentUsers_Content]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContentUsers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContentUsers]'))
ALTER TABLE [dbo].[ContentUsers] DROP CONSTRAINT [FK_ContentUsers_Users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Media_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[Media]'))
ALTER TABLE [dbo].[Media] DROP CONSTRAINT [FK_Media_Content]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Content_SubPages]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pages]'))
ALTER TABLE [dbo].[Pages] DROP CONSTRAINT [FK_Content_SubPages]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Posts_Pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[Posts]'))
ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_Posts_Pages]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UrlRedirects_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[UrlRedirects]'))
ALTER TABLE [dbo].[UrlRedirects] DROP CONSTRAINT [FK_UrlRedirects_Content]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Users_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Users]
GO
/****** Object:  Table [dbo].[Content]    Script Date: 03/16/2006 20:34:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND type in (N'U'))
DROP TABLE [dbo].[Content]
GO
/****** Object:  StoredProcedure [dbo].[users_Save]    Script Date: 03/16/2006 20:34:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users_Save]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[users_Save]
GO
/****** Object:  Table [dbo].[ContentRoles]    Script Date: 03/16/2006 20:34:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentRoles]') AND type in (N'U'))
DROP TABLE [dbo].[ContentRoles]
GO
/****** Object:  Table [dbo].[ContentUsers]    Script Date: 03/16/2006 20:34:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentUsers]') AND type in (N'U'))
DROP TABLE [dbo].[ContentUsers]
GO
/****** Object:  Table [dbo].[Media]    Script Date: 03/16/2006 20:34:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Media]') AND type in (N'U'))
DROP TABLE [dbo].[Media]
GO
/****** Object:  Table [dbo].[Modules]    Script Date: 03/16/2006 20:34:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Modules]') AND type in (N'U'))
DROP TABLE [dbo].[Modules]
GO
/****** Object:  Table [dbo].[Pages]    Script Date: 03/16/2006 20:34:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pages]') AND type in (N'U'))
DROP TABLE [dbo].[Pages]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 03/16/2006 20:34:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Posts]') AND type in (N'U'))
DROP TABLE [dbo].[Posts]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 03/16/2006 20:34:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
DROP TABLE [dbo].[Roles]
GO
/****** Object:  Table [dbo].[Templates]    Script Date: 03/16/2006 20:34:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Templates]') AND type in (N'U'))
DROP TABLE [dbo].[Templates]
GO
/****** Object:  Table [dbo].[UrlRedirects]    Script Date: 03/16/2006 20:34:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UrlRedirects]') AND type in (N'U'))
DROP TABLE [dbo].[UrlRedirects]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 03/16/2006 20:34:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  StoredProcedure [dbo].[content_Delete]    Script Date: 03/16/2006 20:34:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[content_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[content_Delete]
GO
/****** Object:  StoredProcedure [dbo].[content_List]    Script Date: 03/16/2006 20:34:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[content_List]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[content_List]
GO
/****** Object:  StoredProcedure [dbo].[content_Load]    Script Date: 03/16/2006 20:34:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[content_Load]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[content_Load]
GO
/****** Object:  StoredProcedure [dbo].[content_Save]    Script Date: 03/16/2006 20:34:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[content_Save]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[content_Save]
GO
/****** Object:  StoredProcedure [dbo].[content_UpdateChildPaths]    Script Date: 03/16/2006 20:34:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[content_UpdateChildPaths]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[content_UpdateChildPaths]
GO
/****** Object:  StoredProcedure [dbo].[media_Delete]    Script Date: 03/16/2006 20:34:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[media_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[media_Delete]
GO
/****** Object:  StoredProcedure [dbo].[media_List]    Script Date: 03/16/2006 20:34:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[media_List]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[media_List]
GO
/****** Object:  StoredProcedure [dbo].[media_Load]    Script Date: 03/16/2006 20:34:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[media_Load]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[media_Load]
GO
/****** Object:  StoredProcedure [dbo].[media_Save]    Script Date: 03/16/2006 20:34:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[media_Save]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[media_Save]
GO
/****** Object:  StoredProcedure [dbo].[modules_Delete]    Script Date: 03/16/2006 20:34:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modules_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[modules_Delete]
GO
/****** Object:  StoredProcedure [dbo].[modules_List]    Script Date: 03/16/2006 20:34:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modules_List]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[modules_List]
GO
/****** Object:  StoredProcedure [dbo].[modules_Load]    Script Date: 03/16/2006 20:34:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modules_Load]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[modules_Load]
GO
/****** Object:  StoredProcedure [dbo].[modules_Save]    Script Date: 03/16/2006 20:34:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modules_Save]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[modules_Save]
GO
/****** Object:  StoredProcedure [dbo].[pages_Delete]    Script Date: 03/16/2006 20:34:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pages_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pages_Delete]
GO
/****** Object:  StoredProcedure [dbo].[pages_List]    Script Date: 03/16/2006 20:34:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pages_List]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pages_List]
GO
/****** Object:  StoredProcedure [dbo].[pages_Load]    Script Date: 03/16/2006 20:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pages_Load]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pages_Load]
GO
/****** Object:  StoredProcedure [dbo].[pages_Save]    Script Date: 03/16/2006 20:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pages_Save]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pages_Save]
GO
/****** Object:  StoredProcedure [dbo].[posts_Delete]    Script Date: 03/16/2006 20:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[posts_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[posts_Delete]
GO
/****** Object:  StoredProcedure [dbo].[posts_List]    Script Date: 03/16/2006 20:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[posts_List]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[posts_List]
GO
/****** Object:  StoredProcedure [dbo].[posts_Load]    Script Date: 03/16/2006 20:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[posts_Load]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[posts_Load]
GO
/****** Object:  StoredProcedure [dbo].[posts_Save]    Script Date: 03/16/2006 20:34:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[posts_Save]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[posts_Save]
GO
/****** Object:  StoredProcedure [dbo].[roles_Delete]    Script Date: 03/16/2006 20:34:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[roles_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[roles_Delete]
GO
/****** Object:  StoredProcedure [dbo].[roles_List]    Script Date: 03/16/2006 20:34:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[roles_List]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[roles_List]
GO
/****** Object:  StoredProcedure [dbo].[roles_Load]    Script Date: 03/16/2006 20:34:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[roles_Load]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[roles_Load]
GO
/****** Object:  StoredProcedure [dbo].[roles_Save]    Script Date: 03/16/2006 20:34:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[roles_Save]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[roles_Save]
GO
/****** Object:  StoredProcedure [dbo].[templates_List]    Script Date: 03/16/2006 20:34:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[templates_List]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[templates_List]
GO
/****** Object:  StoredProcedure [dbo].[templates_Load]    Script Date: 03/16/2006 20:34:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[templates_Load]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[templates_Load]
GO
/****** Object:  StoredProcedure [dbo].[templates_Save]    Script Date: 03/16/2006 20:34:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[templates_Save]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[templates_Save]
GO
/****** Object:  StoredProcedure [dbo].[urlredirects_Delete]    Script Date: 03/16/2006 20:34:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[urlredirects_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[urlredirects_Delete]
GO
/****** Object:  StoredProcedure [dbo].[urlredirects_List]    Script Date: 03/16/2006 20:34:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[urlredirects_List]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[urlredirects_List]
GO
/****** Object:  StoredProcedure [dbo].[urlredirects_Load]    Script Date: 03/16/2006 20:34:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[urlredirects_Load]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[urlredirects_Load]
GO
/****** Object:  StoredProcedure [dbo].[urlredirects_Save]    Script Date: 03/16/2006 20:34:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[urlredirects_Save]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[urlredirects_Save]
GO
/****** Object:  StoredProcedure [dbo].[users_Delete]    Script Date: 03/16/2006 20:34:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[users_Delete]
GO
/****** Object:  StoredProcedure [dbo].[users_List]    Script Date: 03/16/2006 20:34:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users_List]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[users_List]
GO
/****** Object:  StoredProcedure [dbo].[users_Load]    Script Date: 03/16/2006 20:34:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users_Load]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[users_Load]
GO
/****** Object:  Table [dbo].[Content]    Script Date: 03/16/2006 20:34:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Content](
	[ID] [uniqueidentifier] NOT NULL,
	[ModuleId] [uniqueidentifier] NULL,
	[ModuleControl] [nvarchar](255) NULL,
	[ContentId] [uniqueidentifier] NULL,
	[TemplateId] [uniqueidentifier] NULL,
	[TemplateControl] [nvarchar](255) NULL,
	[Depth] [int] NOT NULL CONSTRAINT [DF_Pages_Depth]  DEFAULT ((0)),
	[Name] [nvarchar](255) NOT NULL,
	[FileName] [nvarchar](50) NOT NULL,
	[Body] [nvarchar](max) NULL,
	[Path] [nvarchar](800) NULL,
	[PublishDate] [datetime] NOT NULL CONSTRAINT [DF_Content_PublishDate]  DEFAULT (getdate()),
	[ModifyDate] [datetime] NOT NULL CONSTRAINT [DF_Content_ModifyDate]  DEFAULT (getdate()),
	[Settings] [xml] NULL,
	[Status] [int] NOT NULL CONSTRAINT [DF_Content_Status]  DEFAULT ((1)),
	[LastUpdate] [timestamp] NOT NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Content] UNIQUE NONCLUSTERED 
(
	[ContentId] ASC,
	[FileName] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

/****** Object:  Trigger [trigger_UpdatePagePaths]    Script Date: 03/16/2006 20:34:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		Kurt Mackey
-- Create date: 11-5-2005
-- Description:	Keeps page paths perty
-- =============================================
CREATE TRIGGER [trigger_UpdatePagePaths] 
ON [dbo].[Content] 
FOR INSERT, UPDATE
AS
	DECLARE @ID uniqueidentifier
	SELECT @ID=Id FROM Inserted
	exec content_UpdateChildPaths @ID





GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The ID of the Parent content for this row.  Null if this is the top level.' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Content', @level2type=N'COLUMN', @level2name=N'ContentId'

GO
/****** Object:  StoredProcedure [dbo].[users_Save]    Script Date: 03/16/2006 20:34:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users_Save]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.users_Save
	(
		@UserName nvarchar(255) output,
		@ContentId uniqueidentifier=null
	)
AS
	IF NOT EXISTS (SELECT UserName FROM Users WHERE UserName=@UserName)
		BEGIN
		INSERT INTO Users (UserName) VALUES(@UserName)
		END
	
	IF (@ContentId IS NOT NULL) 
			AND NOT EXISTS (
					SELECT ContentId FROM ContentUsers WHERE ContentId=@ContentId AND UserName=@UserName
					)
		BEGIN
		INSERT INTO ContentUsers (UserName, ContentId) VALUES(@UserName, @ContentId)
		END' 
END
GO
/****** Object:  Table [dbo].[ContentRoles]    Script Date: 03/16/2006 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContentRoles](
	[ContentId] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](255) NOT NULL,
	[Flag] [int] NOT NULL,
 CONSTRAINT [PK_ContentRoles] PRIMARY KEY CLUSTERED 
(
	[ContentId] ASC,
	[RoleName] ASC,
	[Flag] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ContentUsers]    Script Date: 03/16/2006 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContentUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContentUsers](
	[ContentId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_ContentUsers] PRIMARY KEY CLUSTERED 
(
	[ContentId] ASC,
	[UserName] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Media]    Script Date: 03/16/2006 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Media]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Media](
	[ID] [uniqueidentifier] NOT NULL,
	[ContentId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[PhysicalPath] [nvarchar](255) NULL,
	[MediaType] [nvarchar](255) NULL,
	[MediaSubType] [nvarchar](255) NULL,
	[Timestamp] [datetime] NULL CONSTRAINT [DF_Media_Timestamp]  DEFAULT (getdate()),
 CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

/****** Object:  Index [IX_Media]    Script Date: 03/16/2006 20:34:37 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Media]') AND name = N'IX_Media')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Media] ON [dbo].[Media] 
(
	[ContentId] ASC,
	[FileName] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Modules]    Script Date: 03/16/2006 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Modules]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Modules](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Path] [nvarchar](255) NOT NULL,
	[Settings] [xml] NULL,
	[LastUpdate] [timestamp] NOT NULL,
 CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Pages]    Script Date: 03/16/2006 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Pages](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Pages_Id]  DEFAULT (newid()),
	[ContentId] [uniqueidentifier] NOT NULL,
	[PageNumber] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Body] [nvarchar](max) NULL,
 CONSTRAINT [PK_SubPages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 03/16/2006 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Posts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Posts](
	[Id] [uniqueidentifier] NOT NULL,
	[ContentId] [uniqueidentifier] NOT NULL,
	[Slug] [nvarchar](255) NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Excerpt] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[PostDate] [datetime] NOT NULL CONSTRAINT [DF_Posts_PostDate]  DEFAULT (getdate()),
	[Status] [int] NOT NULL CONSTRAINT [DF_Posts_Status]  DEFAULT ((0)),
	[DiscussionUrl] [nvarchar](255) NULL,
	[MoreUrl] [nvarchar](255) NULL,
 CONSTRAINT [PK_Posts_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 03/16/2006 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Roles](
	[RoleName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Roles_1] PRIMARY KEY CLUSTERED 
(
	[RoleName] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Templates]    Script Date: 03/16/2006 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Templates]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Templates](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Path] [nvarchar](700) NOT NULL,
	[LastUpdate] [timestamp] NOT NULL,
 CONSTRAINT [PK_Templates] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UrlRedirects]    Script Date: 03/16/2006 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UrlRedirects]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UrlRedirects](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_UrlRedirects_Id]  DEFAULT (newid()),
	[Url] [nvarchar](255) NOT NULL,
	[ContentId] [uniqueidentifier] NOT NULL,
	[PathExtra] [nvarchar](255) NULL,
 CONSTRAINT [PK_UrlRedirects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

/****** Object:  Index [IX_UrlRedirects]    Script Date: 03/16/2006 20:34:37 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UrlRedirects]') AND name = N'IX_UrlRedirects')
CREATE UNIQUE NONCLUSTERED INDEX [IX_UrlRedirects] ON [dbo].[UrlRedirects] 
(
	[Url] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 03/16/2006 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[UserName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  StoredProcedure [dbo].[content_Delete]    Script Date: 03/16/2006 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[content_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.content_Delete
	(
	@ID uniqueidentifier
	)
AS
	DELETE FROM Content WHERE Id=@Id
' 
END
GO
/****** Object:  StoredProcedure [dbo].[content_List]    Script Date: 03/16/2006 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[content_List]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.content_List
	(
		@ContentId uniqueidentifier = null,
		@Depth int=1,
		@ModuleId uniqueidentifier = null,
		@ModuleControl nvarchar(255)= null
	)
AS
	SET NOCOUNT ON
	DECLARE @Path nvarchar(800)
	DECLARE @ParentContentId uniqueidentifier
	DECLARE @ContentDepth int
	
	SELECT @ParentContentId=ContentId, @Path=Path, @ContentDepth=Depth FROM Content WHERE ID=@ContentId
	
	IF(@ParentContentId IS NULL)
		SET @Path = ''''
		
	SET @Path = @Path + ''%''
	
	SELECT 
		* 
	FROM 
		Content 
	WHERE 
		Path LIKE @Path AND 
		(Depth-@ContentDepth <= @Depth OR @Depth = -1) AND 
		ID <> @ContentId AND
		(ModuleId=@ModuleId OR @ModuleId IS NULL) AND
		(ModuleControl LIKE @ModuleControl OR @ModuleControl IS NULL)
	ORDER BY 
		Path, [Name]
	RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[content_Load]    Script Date: 03/16/2006 20:34:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[content_Load]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.content_Load
	(
		@Id uniqueidentifier=null,
		@Path nvarchar(800)=null
	)
AS
	SELECT 
		*
	FROM
		Content
	WHERE
		ID=@Id
		OR
		Path=@Path
' 
END
GO
/****** Object:  StoredProcedure [dbo].[content_Save]    Script Date: 03/16/2006 20:34:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[content_Save]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.content_Save
	(
		@Id uniqueidentifier=null output,
		@ContentId uniqueidentifier,
		@PublishDate datetime,
		@ModuleId uniqueidentifier,
		@ModuleControl nvarchar(255),
		@Name nvarchar(255),
		@Body nvarchar(MAX)=null,
		@FileName nvarchar(255),
		@Settings xml=null,
		@TemplateId uniqueidentifier, 
		@TemplateControl nvarchar(255),
		@Status int=0,
		@Path nvarchar(800)=null output
	)
AS
	IF @Id IS NULL OR @Id = Cast(''{00000000-0000-0000-0000-000000000000}'' as uniqueidentifier)
		SET @Id = newid()
	DECLARE @SafeFileName nvarchar(255), @LoopCount int
	SET @SafeFileName = @FileName
	SET @LoopCount = 0
	
	PRINT @FileName + ''-'' + CAST(@LoopCount AS nvarchar(255))
	
	WHILE EXISTS(SELECT ID FROM Content WHERE ContentId=@ContentId AND FileName=@SafeFileName AND ID <> @Id)
		BEGIN
			SET @LoopCount = @LoopCount + 1
			SET @SafeFileName = @FileName + ''-'' + CAST(@LoopCount AS nvarchar(255))
			PRINT @SafeFileName
		END
	IF NOT EXISTS (SELECT ID FROM Content WHERE ID=@Id)
		BEGIN
			INSERT INTO Content (Id,ContentId, ModuleId, ModuleControl, [Name], Body, FileName, PublishDate, Settings, TemplateId, TemplateControl, Status)
			VALUES (@Id,@ContentId, @ModuleId, @ModuleControl, @Name, @Body, @SafeFileName, @PublishDate, @Settings, @TemplateId, @TemplateControl, @Status)
		END
	ELSE
		BEGIN
			UPDATE 
				Content 
			SET 
				ContentId=@ContentId, 
				ModuleId=@ModuleId, 
				ModuleControl=@ModuleControl, 
				[Name]=@Name, 
				Body=@Body, 
				FileName=@SafeFileName, 
				PublishDate=@PublishDate,
				TemplateId=@TemplateId,
				TemplateControl=@TemplateControl,
				Settings=@Settings,
				Status=@Status
			WHERE 
				ID=@Id
		END
		
	SELECT @Path=Path FROM Content WHERE ID=@Id


' 
END
GO
/****** Object:  StoredProcedure [dbo].[content_UpdateChildPaths]    Script Date: 03/16/2006 20:34:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[content_UpdateChildPaths]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[content_UpdateChildPaths]
	@PageId uniqueidentifier
WITH EXECUTE AS CALLER
AS
SET NOCOUNT ON
    DECLARE @ParentId uniqueidentifier
	DECLARE @FileName varchar(50)
	DECLARE @ParentPath varchar(255)
	DECLARE @Depth int
	SET @Depth = 0
	UPDATE Content SET Path=null WHERE ContentId IS NULL
	
	SELECT @ParentId=ContentId, @FileName=FileName FROM Content WHERE ID=@PageId
	
	IF(@ParentId IS NOT NULL)
		BEGIN
			SELECT @ParentPath=Path, @Depth=Depth FROM Content WHERE ID=@ParentId
			SET @Depth = @Depth + 1
		END
	
		
	IF @ParentPath IS NULL OR @ParentPath = ''''
		BEGIN
			IF @ParentId IS NULL
				SET @ParentPath = ''/''
			ELSE
				SET @ParentPath = ''/'' + @FileName
		END
	ELSE
		SET @ParentPath = @ParentPath + ''/'' + @FileName
		
	UPDATE Content SET Path=@ParentPath, Depth=@Depth WHERE ID=@PageId
	
	PRINT @ParentPath
	DECLARE page CURSOR LOCAL FOR
	SELECT ID FROM Content WHERE ContentId=@PageId
	DECLARE @SubPage uniqueidentifier
	OPEN page
	
	FETCH NEXT FROM page INTO @SubPage
	
	WHILE @@FETCH_STATUS = 0
		BEGIN
			exec content_UpdateChildPaths @SubPage
			FETCH NEXT FROM page INTO @SubPage
		END
	CLOSE page
	DEALLOCATE page
	
	UPDATE Content SET Path=''/''+ FileName WHERE ContentId IS NULL
' 
END
GO
/****** Object:  StoredProcedure [dbo].[media_Delete]    Script Date: 03/16/2006 20:34:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[media_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.media_Delete
	(
		@Id uniqueidentifier=null,
		@FileName nvarchar(255)=null,
		@ContentId uniqueidentifier=null
	)
AS
	DELETE
	FROM
		Media
	WHERE
		ID=@ID
		OR
		(FileName=@FileName AND ContentId=@ContentId)
	RETURN' 
END
GO
/****** Object:  StoredProcedure [dbo].[media_List]    Script Date: 03/16/2006 20:34:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[media_List]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure media_List
	(
		@ContentId uniqueidentifier
	)
AS
	SELECT
		*
	FROM
		Media
	WHERE
		ContentId=@ContentId

' 
END
GO
/****** Object:  StoredProcedure [dbo].[media_Load]    Script Date: 03/16/2006 20:34:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[media_Load]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[media_Load]
	(
	@Id uniqueidentifier=null,
	@FileName nvarchar(255)=null,
	@ContentId uniqueidentifier=null
	)
AS
	SELECT
		*
	FROM
		Media
	WHERE
		ID=@ID
		OR
		(FileName=@FileName AND ContentId=@ContentId)
	RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[media_Save]    Script Date: 03/16/2006 20:34:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[media_Save]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure media_Save
	(
	@Id uniqueidentifier=null output,
	@ContentId uniqueidentifier,
	@Title nvarchar(255)=null,
	@FileName nvarchar(255),
	@PhysicalPath nvarchar(255),
	@MediaType nvarchar(255),
	@MediaSubType nvarchar(255)
	)
AS

	IF @Id IS NULL OR @Id = Cast(''{00000000-0000-0000-0000-000000000000}'' as uniqueidentifier)
		SET @Id = newid()
	
	IF NOT EXISTS( SELECT Id FROM Media WHERE Id=@Id)
		INSERT INTO Media (Id, ContentId, Title, FileName, PhysicalPath, MediaType, MediaSubType)
		VALUES (@Id, @ContentId, @Title, @FileName, @PhysicalPath, @MediaType, @MediaSubType)
	ELSE
		UPDATE
			Media
		SET
			ContentId=@ContentId,
			Title=@Title,
			FileName=@FileName,
			PhysicalPath=@PhysicalPath,
			MediaType=@MediaType,
			MediaSubType=@MediaSubType
		WHERE
			Id = @Id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[modules_Delete]    Script Date: 03/16/2006 20:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modules_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.modules_Delete
	(
	@Id uniqueidentifier
	)
AS
	DELETE FROM Modules WHERE Id=@Id
' 
END
GO
/****** Object:  StoredProcedure [dbo].[modules_List]    Script Date: 03/16/2006 20:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modules_List]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[modules_List]
AS
	SELECT
		*
	FROM
		Modules
	ORDER BY
		Name
' 
END
GO
/****** Object:  StoredProcedure [dbo].[modules_Load]    Script Date: 03/16/2006 20:34:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modules_Load]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[modules_Load]
	(
	@Id uniqueidentifier
	)
AS
	SELECT
		*
	FROM
		Modules
	WHERE
		ID=@Id
	RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[modules_Save]    Script Date: 03/16/2006 20:34:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modules_Save]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[modules_Save]
	(
	@Id uniqueidentifier=null output,
	@Name nvarchar(255),
	@Path nvarchar(255),
	@Settings xml=null
	)
AS
	IF @Id IS NULL OR @Id = Cast(''{00000000-0000-0000-0000-000000000000}'' as uniqueidentifier)
		SET @Id = newid()
		
	IF NOT EXISTS( SELECT Id FROM Modules WHERE Id=@Id)
		INSERT INTO Modules (Id, Name, Path, Settings)
		VALUES (@Id, @Name, @Path, @Settings)
	ELSE
		UPDATE
			Modules
		SET
			Name=@Name,
			Path=@Path
		WHERE
			Id=@Id
' 
END
GO
/****** Object:  StoredProcedure [dbo].[pages_Delete]    Script Date: 03/16/2006 20:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pages_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.pages_Delete
	(
		@Id uniqueidentifier,
		@ContentId uniqueidentifier = null
	)
AS
	DELETE
	FROM
		Pages
	WHERE
		ID=@ID
	RETURN' 
END
GO
/****** Object:  StoredProcedure [dbo].[pages_List]    Script Date: 03/16/2006 20:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pages_List]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[pages_List]
	(
		@ContentId uniqueidentifier
	)
AS
	SELECT
		*
	FROM
		Pages
	WHERE
		ContentId=@ContentId
	ORDER BY
		PageNumber

' 
END
GO
/****** Object:  StoredProcedure [dbo].[pages_Load]    Script Date: 03/16/2006 20:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pages_Load]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[pages_Load]
	(
	@Id uniqueidentifier
	)
AS
	SELECT
		*
	FROM
		Pages
	WHERE
		ID=@Id
	RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[pages_Save]    Script Date: 03/16/2006 20:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pages_Save]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE pages_Save
	(
	@Id uniqueidentifier=null output,
	@ContentId uniqueidentifier,
	@PageNumber int,
	@Name nvarchar(255)=null,
	@Body nvarchar(MAX)
	)
AS

	IF @Id IS NULL OR @Id = Cast(''{00000000-0000-0000-0000-000000000000}'' as uniqueidentifier)
		SET @Id = newid()
	
	IF NOT EXISTS( SELECT Id FROM Pages WHERE Id=@Id)
		INSERT INTO Pages (Id, ContentId, PageNumber, Name, Body)
		VALUES (@Id, @ContentId, @PageNumber, @Name, @Body)
	ELSE
		UPDATE
			Pages
		SET
			ContentId=@ContentId,
			Name=@Name,
			PageNumber=@PageNumber,
			Body=@Body
		WHERE
			Id = @Id

' 
END
GO
/****** Object:  StoredProcedure [dbo].[posts_Delete]    Script Date: 03/16/2006 20:34:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[posts_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.posts_Delete
	(
	@Id uniqueidentifier
	)
AS
	DELETE FROM
		Posts
	WHERE
		Id=@Id
' 
END
GO
/****** Object:  StoredProcedure [dbo].[posts_List]    Script Date: 03/16/2006 20:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[posts_List]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE Procedure posts_List
	(
		@ContentId uniqueidentifier
	)
AS
	SELECT
		*
	FROM
		Posts
	WHERE
		ContentId=@ContentId
	ORDER BY
		PostDate DESC

' 
END
GO
/****** Object:  StoredProcedure [dbo].[posts_Load]    Script Date: 03/16/2006 20:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[posts_Load]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[posts_Load]
	(
	@Id uniqueidentifier=null,
	@Slug nvarchar(255)=null,
	@ContentId uniqueidentifier=null
	)
AS
	SELECT
		*
	FROM
		Posts
	WHERE
		ID=@ID
		OR
		(Slug=@Slug AND ContentId=@ContentId)
	RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[posts_Save]    Script Date: 03/16/2006 20:34:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[posts_Save]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.posts_Save
	(
		@Id uniqueidentifier=null output,
		@ContentId uniqueidentifier,
		@PostDate datetime,
		@Status int=0,
		@Slug nvarchar(255) output,
		@UserName nvarchar(255),
		@Title nvarchar(255),
		@Excerpt nvarchar(MAX)=null,
		@Body nvarchar(MAX)=null,
		@MoreUrl nvarchar(255)=null,
		@DiscussionUrl nvarchar(255)=null
	)
AS
	IF @Id IS NULL OR @Id = Cast(''{00000000-0000-0000-0000-000000000000}'' as uniqueidentifier)
		SET @Id = newid()
	DECLARE @SafeSlug nvarchar(255), @LoopCount int
	SET @SafeSlug = @Slug
	SET @LoopCount = 0
	
	WHILE EXISTS(SELECT ID FROM Posts WHERE ContentId=@ContentId AND Slug=@SafeSlug AND ID <> @Id)
		BEGIN
			SET @LoopCount = @LoopCount + 1
			SET @SafeSlug = @Slug + ''-'' + CAST(@LoopCount AS nvarchar(255))
		END
		
	SET @Slug = @SafeSlug
	
	IF NOT EXISTS(SELECT ID FROM Posts WHERE Id=@Id)
		BEGIN
			INSERT INTO Posts(Id, ContentId, Slug, UserName, Title, Excerpt, Body, PostDate, Status, DiscussionUrl, MoreUrl)
			VALUES(@Id, @ContentId, @Slug, @UserName, @Title, @Excerpt, @Body, @PostDate, @Status, @DiscussionUrl, @MoreUrl)
		END
	ELSE
		BEGIN
			UPDATE
				Posts
			SET
				ContentId=@ContentId,
				Slug=@Slug,
				UserName=@UserName,
				Title=@Title,
				Excerpt=@Excerpt,
				Body=@Body,
				PostDate=@PostDate,
				Status=@Status,
				MoreUrl=@MoreUrl,
				DiscussionUrl=@DiscussionUrl
			WHERE
				Id=@Id
		END' 
END
GO
/****** Object:  StoredProcedure [dbo].[roles_Delete]    Script Date: 03/16/2006 20:34:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[roles_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.roles_Delete
	(
	@RoleName nvarchar(255),
	@ContentId uniqueidentifier=null
	)
AS
	IF @ContentId IS NOT null
		DELETE FROM ContentRoles WHERE RoleName=@RoleName AND ContentId=@ContentId
	ELSE
		DELETE FROM Roles WHERE RoleName=@RoleName
	
' 
END
GO
/****** Object:  StoredProcedure [dbo].[roles_List]    Script Date: 03/16/2006 20:34:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[roles_List]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.roles_List
	(
		@ContentId uniqueidentifier=null
	)
AS
	IF @ContentId IS NOT null
		SELECT
			b.*, a.Flag
		FROM
			ContentRoles a, Roles b
		WHERE a.RoleName=b.RoleName
			AND ContentId=@ContentId
		ORDER BY
			a.RoleName, Flag
	ELSE
		SELECT
			*
		FROM
			Roles
		ORDER BY
			RoleName
' 
END
GO
/****** Object:  StoredProcedure [dbo].[roles_Load]    Script Date: 03/16/2006 20:34:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[roles_Load]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.roles_Load
	(
		@RoleName nvarchar(255)
	)
AS
	SELECT 
		*
	FROM
		Roles
	WHERE
		RoleName=@RoleName
' 
END
GO
/****** Object:  StoredProcedure [dbo].[roles_Save]    Script Date: 03/16/2006 20:34:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[roles_Save]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.roles_Save
	(
		@RoleName nvarchar(255) output,
		@ContentId uniqueidentifier=null,
		@Flag int=null
	)
AS
	IF NOT EXISTS (SELECT RoleName FROM Roles WHERE RoleName=@RoleName)
		BEGIN
		INSERT INTO Roles (RoleName) VALUES(@RoleName)
		END
	
	IF (@ContentId IS NOT NULL AND @Flag IS NOT NULL) 
			AND NOT EXISTS (
					SELECT 
						ContentId 
					FROM 
						ContentRoles 
					WHERE ContentId=@ContentId 
						AND RoleName=@RoleName
						AND Flag=@Flag
					)
		BEGIN
		INSERT INTO ContentRoles (RoleName, ContentId, Flag) VALUES(@RoleName, @ContentId, @Flag)
		END' 
END
GO
/****** Object:  StoredProcedure [dbo].[templates_List]    Script Date: 03/16/2006 20:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[templates_List]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[templates_List]
AS
	SELECT
		*
	FROM
		Templates
	ORDER BY
		Name' 
END
GO
/****** Object:  StoredProcedure [dbo].[templates_Load]    Script Date: 03/16/2006 20:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[templates_Load]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[templates_Load]
	(
		@Id uniqueidentifier
	)
AS
	SELECT
		*
	FROM
		Templates
	WHERE
		ID=@Id
	RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[templates_Save]    Script Date: 03/16/2006 20:34:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[templates_Save]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[templates_Save]
	(
		@Id uniqueidentifier=null output,
		@Name nvarchar(255),
		@Path nvarchar(255)
	)
AS

	IF @Id IS NULL OR @Id = Cast(''{00000000-0000-0000-0000-000000000000}'' as uniqueidentifier)
		SET @Id = newid()
		
	IF NOT EXISTS( SELECT Id FROM Templates WHERE Id=@Id )
		INSERT INTO Templates(Id, Name, Path)
		VALUES(@Id, @Name, @Path)
	ELSE
		UPDATE
			Templates
		SET
			Name=@Name,
			Path=@Path
		WHERE
			Id=@Id
' 
END
GO
/****** Object:  StoredProcedure [dbo].[urlredirects_Delete]    Script Date: 03/16/2006 20:34:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[urlredirects_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.urlredirects_Delete
	(
	@Id uniqueidentifier
	)
AS
	DELETE FROM UrlRedirects WHERE ID=@Id	
' 
END
GO
/****** Object:  StoredProcedure [dbo].[urlredirects_List]    Script Date: 03/16/2006 20:34:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[urlredirects_List]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.urlredirects_List 
	(
	@ContentId uniqueidentifier = null,
	@Url nvarchar(255) = null
	)
AS
	IF(@ContentId IS NOT NULL)
		BEGIN
			SELECT
				*
			FROM
				UrlRedirects
			WHERE
				ContentId=@ContentId
			ORDER BY
				Url
			RETURN
		END
	IF(@Url IS NOT NULL)
		BEGIN
			SELECT
				*
			FROM
				UrlRedirects
			WHERE
				@Url LIKE Url
			ORDER BY
				LEN(Url) DESC
			RETURN
		END' 
END
GO
/****** Object:  StoredProcedure [dbo].[urlredirects_Load]    Script Date: 03/16/2006 20:34:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[urlredirects_Load]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.urlredirects_Load
	(
	@ID uniqueidentifier
	)
AS
	SELECT
		*
	FROM
		UrlRedirects
	WHERE
		ID=@ID' 
END
GO
/****** Object:  StoredProcedure [dbo].[urlredirects_Save]    Script Date: 03/16/2006 20:34:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[urlredirects_Save]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.urlredirects_Save 
	(
		@Id uniqueidentifier=null output,
		@Url nvarchar(255),
		@ContentId uniqueidentifier,
		@PathExtra nvarchar(255)=null
	)
AS
	IF @Id IS NULL OR @Id = Cast(''{00000000-0000-0000-0000-000000000000}'' as uniqueidentifier)
		SET @Id = newid()
	
	IF NOT EXISTS( SELECT Id FROM UrlRedirects WHERE Id=@Id OR Url=@Url)
		INSERT INTO UrlRedirects(Id, Url, ContentId, PathExtra)
		VALUES(@Id, @Url, @ContentId, @PathExtra)
	ELSE
		UPDATE
			UrlRedirects
		SET
			Url=@Url, ContentId=@ContentId, PathExtra=@PathExtra
		WHERE Id=@Id OR Url=@Url
' 
END
GO
/****** Object:  StoredProcedure [dbo].[users_Delete]    Script Date: 03/16/2006 20:34:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.users_Delete
	(
	@UserName nvarchar(255),
	@ContentId uniqueidentifier=null
	)
AS
	IF @ContentId IS NOT null
		DELETE FROM ContentUsers WHERE UserName=@UserName AND ContentId=@ContentId
	ELSE
		DELETE FROM Users WHERE UserName=@UserName
	
' 
END
GO
/****** Object:  StoredProcedure [dbo].[users_List]    Script Date: 03/16/2006 20:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users_List]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.users_List
	(
		@ContentId uniqueidentifier=null
	)
AS
	IF @ContentId IS NOT null
		SELECT
			*
		FROM
			Users
		WHERE
			UserName IN
				(SELECT UserName FROM ContentUsers WHERE ContentId=@ContentId)
		ORDER BY
			UserName
	ELSE
		SELECT
			*
		FROM
			Users
		ORDER BY
			UserName
' 
END
GO
/****** Object:  StoredProcedure [dbo].[users_Load]    Script Date: 03/16/2006 20:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users_Load]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE dbo.users_Load
	(
		@UserName nvarchar(255)
	)
AS
	SELECT 
		*
	FROM
		Users
	WHERE
		UserName=@UserName
' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Content_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
ALTER TABLE [dbo].[Content]  WITH CHECK ADD  CONSTRAINT [FK_Content_Content] FOREIGN KEY([ContentId])
REFERENCES [dbo].[Content] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Content_Modules]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
ALTER TABLE [dbo].[Content]  WITH NOCHECK ADD  CONSTRAINT [FK_Content_Modules] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Modules] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Content] CHECK CONSTRAINT [FK_Content_Modules]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Template_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
ALTER TABLE [dbo].[Content]  WITH NOCHECK ADD  CONSTRAINT [FK_Template_Content] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Templates] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Content] CHECK CONSTRAINT [FK_Template_Content]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContentRoles_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContentRoles]'))
ALTER TABLE [dbo].[ContentRoles]  WITH CHECK ADD  CONSTRAINT [FK_ContentRoles_Content] FOREIGN KEY([ContentId])
REFERENCES [dbo].[Content] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContentRoles_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContentRoles]'))
ALTER TABLE [dbo].[ContentRoles]  WITH CHECK ADD  CONSTRAINT [FK_ContentRoles_Roles] FOREIGN KEY([RoleName])
REFERENCES [dbo].[Roles] ([RoleName])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContentUsers_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContentUsers]'))
ALTER TABLE [dbo].[ContentUsers]  WITH NOCHECK ADD  CONSTRAINT [FK_ContentUsers_Content] FOREIGN KEY([ContentId])
REFERENCES [dbo].[Content] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContentUsers] CHECK CONSTRAINT [FK_ContentUsers_Content]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContentUsers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContentUsers]'))
ALTER TABLE [dbo].[ContentUsers]  WITH CHECK ADD  CONSTRAINT [FK_ContentUsers_Users] FOREIGN KEY([UserName])
REFERENCES [dbo].[Users] ([UserName])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Media_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[Media]'))
ALTER TABLE [dbo].[Media]  WITH NOCHECK ADD  CONSTRAINT [FK_Media_Content] FOREIGN KEY([ContentId])
REFERENCES [dbo].[Content] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Media] CHECK CONSTRAINT [FK_Media_Content]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Content_SubPages]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pages]'))
ALTER TABLE [dbo].[Pages]  WITH NOCHECK ADD  CONSTRAINT [FK_Content_SubPages] FOREIGN KEY([ContentId])
REFERENCES [dbo].[Content] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Content_SubPages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Posts_Pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[Posts]'))
ALTER TABLE [dbo].[Posts]  WITH NOCHECK ADD  CONSTRAINT [FK_Posts_Pages] FOREIGN KEY([ContentId])
REFERENCES [dbo].[Content] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Pages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UrlRedirects_Content]') AND parent_object_id = OBJECT_ID(N'[dbo].[UrlRedirects]'))
ALTER TABLE [dbo].[UrlRedirects]  WITH CHECK ADD  CONSTRAINT [FK_UrlRedirects_Content] FOREIGN KEY([ContentId])
REFERENCES [dbo].[Content] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Users_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Users] FOREIGN KEY([UserName])
REFERENCES [dbo].[Users] ([UserName])
GO
