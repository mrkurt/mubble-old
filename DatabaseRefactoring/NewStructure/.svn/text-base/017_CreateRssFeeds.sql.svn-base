/****** Object:  Table [dbo].[RssFeeds]    Script Date: 12/21/2007 18:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RssFeeds](
	[ID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_RssFeeds_ID]  DEFAULT (newsequentialid()),
	[Title] [nvarchar](255) NOT NULL,
	[Link] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ManagingEditor] [nvarchar](255) NULL,
	[ItemFormat] [nvarchar](max) NULL,
	[Slug] [nvarchar](50) NULL,
	[RedirectUrl] [nvarchar](255) NULL,
	[RedirectExceptions] [nvarchar](max) NULL,
	[ControllerID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_RssFeeds] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_UNIQUE_RssFeeds_ControllerID_Slug] UNIQUE NONCLUSTERED 
(
	[ControllerID] ASC,
	[Slug] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RssFeeds]  WITH CHECK ADD  CONSTRAINT [FK_Controllers_RssFeeds] FOREIGN KEY([ControllerID])
REFERENCES [dbo].[Controllers] ([ContentItemID])
GO
ALTER TABLE [dbo].[RssFeeds] CHECK CONSTRAINT [FK_Controllers_RssFeeds]