/*
   Friday, December 21, 20071:15:16 PM
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
select Has_Perms_By_Name(N'dbo.ContentItems', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ContentItems', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ContentItems', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.UrlRedirects
	(
	ID uniqueidentifier NOT NULL,
	Url nvarchar(255) NOT NULL,
	PathExtra nvarchar(255) NULL,
	ContentItemID uniqueidentifier NOT NULL,
	Handler nvarchar(255) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.UrlRedirects ADD CONSTRAINT
	DF_UrlRedirects_ID DEFAULT newsequentialid() FOR ID
GO
ALTER TABLE dbo.UrlRedirects ADD CONSTRAINT
	PK_UrlRedirects PRIMARY KEY NONCLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.UrlRedirects ADD CONSTRAINT
	FK_UrlRedirects_ContentItems FOREIGN KEY
	(
	ContentItemID
	) REFERENCES dbo.ContentItems
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.UrlRedirects', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.UrlRedirects', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.UrlRedirects', 'Object', 'CONTROL') as Contr_Per 