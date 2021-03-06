/*
   Friday, December 21, 200710:24:55 AM
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
CREATE TABLE dbo.ContentItems
	(
	ID uniqueidentifier NOT NULL,
	ContentItemID uniqueidentifier NULL,
	FileName nvarchar(255) NOT NULL,
	Path nvarchar(4000) NOT NULL,
	PublishDate datetime NOT NULL,
	Status int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.ContentItems ADD CONSTRAINT
	DF_ContentItems_ID DEFAULT newsequentialid() FOR ID
GO
ALTER TABLE dbo.ContentItems ADD CONSTRAINT
	DF_ContentItems_PublishDate DEFAULT getdate() FOR PublishDate
GO
ALTER TABLE dbo.ContentItems ADD CONSTRAINT
	DF_ContentItems_Status DEFAULT 0 FOR Status
GO
ALTER TABLE dbo.ContentItems ADD CONSTRAINT
	PK_ContentItems PRIMARY KEY NONCLUSTERED 
	(
	ID
	)

GO

ALTER TABLE dbo.ContentItems ADD CONSTRAINT
	FK_ContentItems_ContentItems FOREIGN KEY
	(
	ContentItemID
	) REFERENCES dbo.ContentItems
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
