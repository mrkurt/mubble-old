/*
   Saturday, December 22, 200710:36:03 AM
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

ALTER TABLE dbo.ContentItems ADD ContentTypeID uniqueidentifier NOT NULL
ALTER TABLE dbo.ContentItems 

ADD CONSTRAINT
	FK_ContentItems_ContentTypes FOREIGN KEY
	(
	ContentTypeID
	) REFERENCES dbo.ContentTypes
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT