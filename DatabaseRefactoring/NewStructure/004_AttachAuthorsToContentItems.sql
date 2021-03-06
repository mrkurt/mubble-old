/*
   Friday, December 21, 20071:01:58 PM
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
select Has_Perms_By_Name(N'dbo.Authors', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Authors', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Authors', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ContentItems', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ContentItems', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ContentItems', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.ContentItemAuthors
	(
	ContentItemID uniqueidentifier NOT NULL,
	AuthorID uniqueidentifier NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.ContentItemAuthors ADD CONSTRAINT
	PK_ContentItemAuthors PRIMARY KEY NONCLUSTERED 
	(
	ContentItemID,
	AuthorID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.ContentItemAuthors ADD CONSTRAINT
	FK_ContentItemAuthors_ContentItems FOREIGN KEY
	(
	ContentItemID
	) REFERENCES dbo.ContentItems
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ContentItemAuthors ADD CONSTRAINT
	FK_ContentItemAuthors_Authors FOREIGN KEY
	(
	AuthorID
	) REFERENCES dbo.Authors
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ContentItemAuthors', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ContentItemAuthors', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ContentItemAuthors', 'Object', 'CONTROL') as Contr_Per 