insert into Authors
select * from arstechnica..Authors where id not in (SELECT ID FROM authors)

insert into Templates
select * from arstechnica..Templates where id not in (SELECT ID FROM Templates)

insert into Modules
select * from arstechnica..Modules where id not in (SELECT ID FROM Modules)

insert into ModuleControls
select * from arstechnica..ModuleControls where id not in (SELECT ID FROM ModuleControls)

insert into DiscussionProviders
select * from arstechnica..DiscussionProviders where id not in (SELECT ID FROM DiscussionProviders)

insert into PermissionFlags
select * from arstechnica..PermissionFlags where id not in (SELECT ID FROM PermissionFlags)

insert into Routes
select * from arstechnica..Routes where id not in (SELECT ID FROM Routes)

alter table TextBlocks add ContentItemID uniqueidentifier null
GO

insert into TextBlocks (Title, Excerpt, Body, ContentItemID)
select Title, Excerpt, Body, ID from arstechnica..Controllers where
ID NOT IN (SELECT ID FROM Controllers)

insert into TextBlocks (Title, Excerpt, Body, ContentItemID)
select Title, Excerpt, Body, ID from arstechnica..Posts where
ID NOT IN (SELECT ID FROM ContentItems)

insert into TextBlocks (Title, Body, ContentItemID)
select Name, Body, ID from arstechnica..Pages where
ID NOT IN (SELECT ID FROM Pages)

create index textblocks_ContentItemID ON TextBlocks(ContentItemID)

insert into ContentItems(ID, ContentItemID, FileName, Path, PublishDate, Status, ContentTypeID, TextBlockID)
select a.id, ControllerID, FileName, Path, PublishDate, Status, b.ID as ContentTypeID, c.ID as TextBlockID
from arstechnica..Controllers a
LEFT OUTER JOIN TextBlocks c ON (c.ContentItemID = a.ID)
LEFT OUTER JOIN ContentTypes b ON (b.ActiveObjectType=a.ActiveObjectType or (a.ActiveObjectType IS NULL AND b.ActiveObjectType is null))
where 
a.ID NOT IN (SELECT ID FROM ContentItems)

insert into Controllers(ContentItemID, TemplateID, TemplateControl, ModuleControlID, ModuleControlView, RouteID, IsContent, IsContentContainer, Settings)
select id as contentitemid, templateid, templatecontrol, modulecontrolid, modulecontrolview, routeid, iscontent, iscontentcontainer, settings
from arstechnica..Controllers where id not in (SELECT ContentItemID FROM Controllers)

insert into ContentItems(ID, ContentItemID, FileName, Path, PublishDate, Status, ContentTypeID, TextBlockID)
select a.ID, ControllerID, Slug as FileName, '' as Path, PublishDate, Status, b.ID as ContentTypeID, c.ID
from arstechnica..Posts a, ContentTypes b, TextBlocks c where b.ActiveObjectType = 'Mubble.Models.Post, Mubble.Models'
and a.ID NOT IN (SELECT ID FROM ContentItems) AND c.ContentItemID=a.ID


insert into Pages (ID, PageNumber, ContentItemID, TextBlockID)
select a.ID, a.PageNumber, a.ControllerID, b.ID from arstechnica..Pages a, TextBlocks b
where a.ID = b.ContentItemID AND a.ID NOT IN (SELECT ID FROM Pages)

drop index textblocks_ContentItemID on TextBlocks

alter table TextBlocks drop column ContentItemID

insert into authors(username, displayname)
select distinct StringValue as username, stringvalue as displayname from arstechnica..Tags
where stringvalue not in (select username from Authors) and name='Author'

insert into authors(username, displayname)
select distinct UserName, UserName as displayname from arstechnica..Posts
where username not in (select username from Authors)

insert into ContentItemAuthors(ContentItemID, AuthorID)
select distinct a.ActiveObjectID, b.ID FROM arstechnica..Tags a,
Authors b where a.StringValue=b.UserName and 
a.ActiveObjectID NOT IN (SELECT ContentItemID FROM ContentItemAuthors WHERE AuthorID = b.ID)

insert into ContentItemAuthors(ContentItemID, AuthorID)
select distinct a.ID, b.ID FROM arstechnica..Posts a,
Authors b where a.UserName=b.UserName and 
a.ID NOT IN (SELECT ContentItemID FROM ContentItemAuthors  WHERE AuthorID = b.ID)

insert into Tags
select ID, ActiveObjectID as ContentItemID, Name, StringValue, StringValueNormalized, 
NumericValue, NormalizeStringValue
from arstechnica..Tags where Name <> 'Author' and ID not in (SELECT ID FROM Tags) and ActiveObjectID in (SELECT ID FROM ContentItems)

insert into RssFeeds
select * from arstechnica..RssFeeds WHERE ID NOT IN (SELECT ID FROM RssFeeds)

insert into Permissions
select ID, ActiveObjectID as ControllerID, [Group], Flag from arstechnica..Permissions WHERE ID NOT IN (SELECT ID FROM Permissions)

insert into Files
select id, ControllerID as ContentItemID, Name, FileName, PhysicalPath, MediaType, MediaSubType
from arstechnica..Files where ID not in (SELECT ID FROM Files)

insert into Discussions
select id, ActiveObjectID as ContentItemId, DiscussionProviderID, DiscussionLink, Status, LastStatusMessage, CommentCount
from arstechnica..Discussions where id not in (SELECT ID FROM Discussions) and ActiveObjectID in (SELECT ID FROM ContentItems)