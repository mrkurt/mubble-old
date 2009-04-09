/****** Object:  Table [dbo].[Templates]    Script Date: 12/21/2007 18:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Templates](
	[ID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Templates_ID]  DEFAULT (newsequentialid()),
	[Name] [nvarchar](255) NOT NULL,
	[Path] [nvarchar](255) NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Templates] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO