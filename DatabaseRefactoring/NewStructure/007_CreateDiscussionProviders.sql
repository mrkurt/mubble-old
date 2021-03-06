/*
   Friday, December 21, 20075:24:13 PM
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
CREATE TABLE dbo.DiscussionProviders
	(
	ID uniqueidentifier NOT NULL,
	Name nvarchar(255) NOT NULL,
	ActiveObjectType nvarchar(255) NULL,
	Settings nvarchar(MAX) NULL,
	IsDefault bit NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.DiscussionProviders ADD CONSTRAINT
	DF_DiscussionProviders_ID DEFAULT newsequentialid() FOR ID
GO
ALTER TABLE dbo.DiscussionProviders ADD CONSTRAINT
	DF_DiscussionProviders_IsDefault DEFAULT 0 FOR IsDefault
GO
ALTER TABLE dbo.DiscussionProviders ADD CONSTRAINT
	PK_DiscussionProviders PRIMARY KEY NONCLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.DiscussionProviders', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.DiscussionProviders', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.DiscussionProviders', 'Object', 'CONTROL') as Contr_Per 