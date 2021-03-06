/*
   Saturday, December 22, 200710:34:20 AM
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
CREATE TABLE dbo.ContentTypes
	(
	ID uniqueidentifier NOT NULL,
	SystemType nvarchar(255) NULL,
	ActiveObjectType nvarchar(255) NULL
	)  ON [PRIMARY]
GO

ALTER TABLE dbo.ContentTypes ADD CONSTRAINT
	DF_ContentTypes_ID DEFAULT newsequentialid() FOR ID
GO

ALTER TABLE dbo.ContentTypes ADD CONSTRAINT
	PK_ContentTypes PRIMARY KEY NONCLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.ContentTypes', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ContentTypes', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ContentTypes', 'Object', 'CONTROL') as Contr_Per 