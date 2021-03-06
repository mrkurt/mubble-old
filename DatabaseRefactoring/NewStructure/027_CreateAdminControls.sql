/****** Object:  Table [dbo].[AdminControls]    Script Date: 01/13/2008 22:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminControls](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[Order] [int] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[ModuleControlID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AdminControls] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_UNIQUE_AdminControls_ModuleControlID_FileName] UNIQUE NONCLUSTERED 
(
	[ModuleControlID] ASC,
	[FileName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AdminControls]  WITH CHECK ADD  CONSTRAINT [FK_ModuleControls_AdminControls] FOREIGN KEY([ModuleControlID])
REFERENCES [dbo].[ModuleControls] ([ID])
GO
ALTER TABLE [dbo].[AdminControls] CHECK CONSTRAINT [FK_ModuleControls_AdminControls]