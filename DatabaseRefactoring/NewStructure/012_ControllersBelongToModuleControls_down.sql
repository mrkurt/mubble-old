/*
   Friday, December 21, 20075:38:59 PM
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
ALTER TABLE dbo.Controllers
	DROP CONSTRAINT FK_Controllers_ModuleControls
GO
COMMIT
BEGIN TRANSACTION
GO
EXECUTE sp_dropextendedproperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Controllers', NULL, NULL
GO
COMMIT
