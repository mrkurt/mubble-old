/****** Object:  Table [dbo].[Modules]    Script Date: 12/21/2007 17:31:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modules](
	[ID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Modules_ID]  DEFAULT (newsequentialid()),
	[Name] [nvarchar](255) NOT NULL,
	[Path] [nvarchar](255) NOT NULL,
	[UpdatedAt] [datetime] NULL CONSTRAINT [DF_Modules_UpdatedAt]  DEFAULT (getdate()),
 CONSTRAINT [PK_Modules] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]