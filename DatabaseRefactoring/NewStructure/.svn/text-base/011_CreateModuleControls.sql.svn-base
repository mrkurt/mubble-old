/****** Object:  Table [dbo].[ModuleControls]    Script Date: 12/21/2007 17:34:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleControls](
	[ID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ModuleControls_ID]  DEFAULT (newsequentialid()),
	[Order] [int] NULL,
	[Name] [nvarchar](255) NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[Type] [nvarchar](255) NULL,
	[ModuleID] [uniqueidentifier] NOT NULL,
	[ControllerActiveObjectType] [nvarchar](255) NULL,
	[IsContent] [bit] NULL CONSTRAINT [DF_ModuleControls_IsContent]  DEFAULT ((0)),
	[IsContentContainer] [bit] NULL CONSTRAINT [DF_ModuleControls_IsContentContainer]  DEFAULT ((0)),
 CONSTRAINT [PK_ModuleControls] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_UNIQUE_ModuleControls_ModuleID_FileName] UNIQUE NONCLUSTERED 
(
	[ModuleID] ASC,
	[FileName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ModuleControls]  WITH CHECK ADD  CONSTRAINT [FK_Modules_ModuleControls] FOREIGN KEY([ModuleID])
REFERENCES [dbo].[Modules] ([ID])
GO
ALTER TABLE [dbo].[ModuleControls] CHECK CONSTRAINT [FK_Modules_ModuleControls]