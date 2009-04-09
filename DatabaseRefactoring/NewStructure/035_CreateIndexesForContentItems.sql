CREATE NONCLUSTERED INDEX [IX_ContentItems_Parents] ON [dbo].[ContentItems] 
(
	[ContentItemID] ASC
)


CREATE CLUSTERED INDEX [IX_ContentItems_PublishDate-DESC] ON [dbo].[ContentItems] 
(
	[PublishDate] DESC
)

CREATE NONCLUSTERED INDEX [IX_ContentItems_ContentType] ON dbo.ContentItems
(
	[ContentTypeID]
)

/****** Object:  Index [IX_UNIQUE_Posts_ControllerID_Slug]    Script Date: 01/15/2008 15:25:14 ******/
ALTER TABLE [dbo].[ContentItems] ADD  CONSTRAINT [IX_UNIQUE_ContentItems_ContentItemID_FileName] UNIQUE NONCLUSTERED 
(
	[ContentItemID] ASC,
	[FileName] ASC
)

CREATE NONCLUSTERED INDEX [IX_ContentItemAuthors_ContentItemID] on ContentItemAuthors(ContentItemID)

CREATE CLUSTERED INDEX [IX_Authors_ID] on Authors(ID)

CREATE NONCLUSTERED INDEX [IX_Tags_ContentItemID] on Tags(ContentItemID)

CREATE NONCLUSTERED INDEX [IX_Discussions_ContentItemID] on Tags(ContentItemID)