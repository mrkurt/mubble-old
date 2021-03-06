/*
   Friday, December 21, 20071:19:19 PM
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
CREATE TABLE dbo.Tags
	(
	ID uniqueidentifier NOT NULL,
	ContentItemID uniqueidentifier NOT NULL,
	Name nvarchar(255) NOT NULL,
	StringValue nvarchar(255) NULL,
	StringValueNormalized nvarchar(255) NULL,
	NumericValue float(53) NOT NULL,
	NormalizeStringValue bit NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tags ADD CONSTRAINT
	DF_Tags_ID DEFAULT newsequentialid() FOR ID
GO
ALTER TABLE dbo.Tags ADD CONSTRAINT
	DF_Tags_NormalizeStringValue DEFAULT 1 FOR NormalizeStringValue
GO
ALTER TABLE dbo.Tags ADD CONSTRAINT
	PK_Tags PRIMARY KEY NONCLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Tags ADD CONSTRAINT
	FK_Tags_ContentItems FOREIGN KEY
	(
	ContentItemID
	) REFERENCES dbo.ContentItems
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Tags', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Tags', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Tags', 'Object', 'CONTROL') as Contr_Per 