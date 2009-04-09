/****** Object:  Table [dbo].[Routes]    Script Date: 12/21/2007 18:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Routes](
	[ID] [uniqueidentifier] NOT NULL,
	[Pattern] [nvarchar](max) NOT NULL,
	[ModuleControlID] [uniqueidentifier] NOT NULL,
	[Order] [int] NULL,
	[IsDefault] [bit] NOT NULL CONSTRAINT [DF_Routes_IsDefault]  DEFAULT ((0)),
	[Name] [nvarchar](255) NULL,
	[FormatString] [nvarchar](255) NULL,
 CONSTRAINT [PK_Routes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE dbo.Routes ADD CONSTRAINT
	DF_Routes_ID DEFAULT newsequentialid() FOR ID
GO

ALTER TABLE [dbo].[Routes]  WITH CHECK ADD  CONSTRAINT [FK_ModuleControls_Routes] FOREIGN KEY([ModuleControlID])
REFERENCES [dbo].[ModuleControls] ([ID])
GO
ALTER TABLE [dbo].[Routes] CHECK CONSTRAINT [FK_ModuleControls_Routes]