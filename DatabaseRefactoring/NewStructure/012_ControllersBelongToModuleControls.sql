/*
   Friday, December 21, 20075:37:28 PM
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
select Has_Perms_By_Name(N'dbo.ModuleControls', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ModuleControls', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ModuleControls', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
DECLARE @v sql_variant 
SET @v = N'Inherits from ContentItems'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Controllers', NULL, NULL
GO
ALTER TABLE dbo.Controllers ADD CONSTRAINT
	FK_Controllers_ModuleControls FOREIGN KEY
	(
	ModuleControlID
	) REFERENCES dbo.ModuleControls
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Controllers', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Controllers', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Controllers', 'Object', 'CONTROL') as Contr_Per 