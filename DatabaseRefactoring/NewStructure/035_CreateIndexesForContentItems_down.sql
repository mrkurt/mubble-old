drop index IX_ContentItems_Parents ON ContentItems
drop index [IX_ContentItems_PublishDate-DESC] ON ContentItems
drop index [IX_ContentItems_ContentType] ON dbo.ContentItems

ALTER TABLE [dbo].[ContentItems] DROP CONSTRAINT [IX_UNIQUE_ContentItems_ContentItemID_FileName]

drop INDEX [IX_ContentItemAuthors_ContentItemID] on ContentItemAuthors

drop INDEX [IX_Authors_ID] on Authors

drop INDEX [IX_Tags_ContentItemID] on Tags

drop INDEX [IX_Discussions_ContentItemID] on Tags