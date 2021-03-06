/*
   Friday, December 21, 20075:28:45 PM
   User: 
   Server: MOBILEVISTA\SQLEXPRESS
   Database: mubble
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
COMMIT
select Has_Perms_By_Name(N'dbo.DiscussionProviders', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.DiscussionProviders', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.DiscussionProviders', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ContentItems', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ContentItems', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ContentItems', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Discussions
	(
	ID uniqueidentifier NOT NULL,
	ContentItemID uniqueidentifier NOT NULL,
	DiscussionProviderID uniqueidentifier NOT NULL,
	DiscussionLink nvarchar(255) NULL,
	Status int NOT NULL,
	LastStatusMessage nvarchar(MAX) NULL,
	CommentCount int NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Discussions ADD CONSTRAINT
	DF_Discussions_ID DEFAULT newsequentialid() FOR ID
GO
ALTER TABLE dbo.Discussions ADD CONSTRAINT
	DF_Discussions_Status DEFAULT 0 FOR Status
GO
ALTER TABLE dbo.Discussions ADD CONSTRAINT
	DF_Discussions_CommentCount DEFAULT 0 FOR CommentCount
GO
ALTER TABLE dbo.Discussions ADD CONSTRAINT
	PK_Discussions PRIMARY KEY NONCLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Discussions ADD CONSTRAINT
	FK_Discussions_ContentItems FOREIGN KEY
	(
	ContentItemID
	) REFERENCES dbo.ContentItems
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Discussions ADD CONSTRAINT
	FK_Discussions_DiscussionProviders FOREIGN KEY
	(
	DiscussionProviderID
	) REFERENCES dbo.DiscussionProviders
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Discussions', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Discussions', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Discussions', 'Object', 'CONTROL') as Contr_Per 