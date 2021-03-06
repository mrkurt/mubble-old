/*
   Tuesday, January 15, 20086:00:04 PM
   User: 
   Server: MRKURT\SQLEXPRESS
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
CREATE TABLE dbo.GuidMapping
	(
	ID uniqueidentifier NOT NULL,
	OldID uniqueidentifier NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.GuidMapping ADD CONSTRAINT
	DF_GuidMapping_ID DEFAULT newsequentialid() FOR ID
GO
ALTER TABLE dbo.GuidMapping ADD CONSTRAINT
	PK_GuidMapping PRIMARY KEY CLUSTERED 
	(
	OldID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.GuidMapping', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.GuidMapping', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.GuidMapping', 'Object', 'CONTROL') as Contr_Per 