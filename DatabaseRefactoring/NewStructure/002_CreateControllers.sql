/*
   Friday, December 21, 200712:28:37 PM
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
CREATE TABLE dbo.Controllers
	(
	ContentItemID uniqueidentifier NOT NULL,
	TemplateID uniqueidentifier NOT NULL,
	TemplateControl nvarchar(255) NOT NULL,
	ModuleControlID uniqueidentifier NOT NULL,
	ModuleControlView nvarchar(255) NULL,
	RouteID uniqueidentifier NULL,
	IsContent bit NOT NULL,
	IsContentContainer bit NOT NULL,
	Settings nvarchar(MAX) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Controllers ADD CONSTRAINT
	DF_Controllers_IsContent DEFAULT 0 FOR IsContent
GO
ALTER TABLE dbo.Controllers ADD CONSTRAINT
	DF_Controllers_IsContentContainer DEFAULT 0 FOR IsContentContainer
GO
ALTER TABLE dbo.Controllers ADD CONSTRAINT
	PK_Controllers PRIMARY KEY NONCLUSTERED 
	(
	ContentItemID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Controllers ADD CONSTRAINT
	FK_Controllers_ContentItems FOREIGN KEY
	(
	ContentItemID
	) REFERENCES dbo.ContentItems
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Controllers', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Controllers', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Controllers', 'Object', 'CONTROL') as Contr_Per 