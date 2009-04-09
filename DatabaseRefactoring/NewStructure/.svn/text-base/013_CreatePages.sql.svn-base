/****** Object:  Table [dbo].[Pages]    Script Date: 12/21/2007 17:43:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pages](
	[ID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Pages_ID]  DEFAULT (newsequentialid()),
	[PageNumber] [int] NOT NULL,
	[ContentItemID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_ContentItems_Pages] FOREIGN KEY([ContentItemID])
REFERENCES [dbo].[ContentItems] ([ID])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_ContentItems_Pages]