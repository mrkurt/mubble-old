/****** Object:  Table [dbo].[Permissions]    Script Date: 12/21/2007 17:54:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[ID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Permissions_ID]  DEFAULT (newsequentialid()),
	[ControllerID] [uniqueidentifier] NOT NULL,
	[Group] [nvarchar](255) NOT NULL,
	[Flag] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Controllers_Permissions] FOREIGN KEY([ControllerID])
REFERENCES [dbo].[Controllers] ([ContentItemID])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Controllers_Permissions]