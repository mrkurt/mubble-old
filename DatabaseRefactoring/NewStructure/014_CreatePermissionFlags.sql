
/****** Object:  Table [dbo].[PermissionFlags]    Script Date: 12/21/2007 17:52:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionFlags](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Flag] [nvarchar](255) NOT NULL,
	[ModuleID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PermissionFlags] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_UNIQUE_PermissionFlags_ModuleID_Flag] UNIQUE NONCLUSTERED 
(
	[ModuleID] ASC,
	[Flag] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE dbo.PermissionFlags ADD CONSTRAINT
	DF_PermissionFlags_ID DEFAULT newsequentialid() FOR ID
GO

ALTER TABLE [dbo].[PermissionFlags]  WITH CHECK ADD  CONSTRAINT [FK_Modules_PermissionFlags] FOREIGN KEY([ModuleID])
REFERENCES [dbo].[Modules] ([ID])
GO
ALTER TABLE [dbo].[PermissionFlags] CHECK CONSTRAINT [FK_Modules_PermissionFlags]