/*
   Friday, January 11, 20083:32:59 PM
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
ALTER TABLE dbo.ContentItems
	DROP CONSTRAINT FK_ContentItems_TextBlocks
GO
ALTER TABLE dbo.Pages
	DROP CONSTRAINT FK_Pages_TextBlocks
GO
COMMIT
BEGIN TRANSACTION
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ContentItems', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ContentItems', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ContentItems', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Pages', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Pages', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Pages', 'Object', 'CONTROL') as Contr_Per 
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ContentItems
	DROP COLUMN TextBlockID
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Pages
	DROP COLUMN TextBlockID
GO
COMMIT