/****** Object:  Table [dbo].[ScheduledCommands]    Script Date: 12/21/2007 18:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScheduledCommands](
	[ID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ScheduledCommands_ID]  DEFAULT (newsequentialid()),
	[RunAt] [datetime] NOT NULL,
	[ContentItemID] [uniqueidentifier] NOT NULL,
	[Command] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ScheduledCommands] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ScheduledCommands]  WITH CHECK ADD  CONSTRAINT [FK_ContentItems_ScheduledCommands] FOREIGN KEY([ContentItemID])
REFERENCES [dbo].[ContentItems] ([ID])
GO
ALTER TABLE [dbo].[ScheduledCommands] CHECK CONSTRAINT [FK_ContentItems_ScheduledCommands]
