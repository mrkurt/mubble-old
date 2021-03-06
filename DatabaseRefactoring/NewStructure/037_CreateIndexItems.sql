/*
   Friday, January 18, 200811:03:34 AM
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
CREATE TABLE dbo.IndexItems
	(
	ContentItemID uniqueidentifier NOT NULL,
	[Document] xml NOT NULL,
	LastUpdated datetime NOT NULL,
	Status int NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.IndexItems ADD CONSTRAINT
	DF_IndexItems_LastUpdated DEFAULT getdate() FOR LastUpdated
GO
ALTER TABLE dbo.IndexItems ADD CONSTRAINT
	DF_IndexItems_Status DEFAULT 1 FOR [Status]
GO
ALTER TABLE dbo.IndexItems ADD CONSTRAINT
	PK_IndexItems PRIMARY KEY CLUSTERED 
	(
	ContentItemID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
--ALTER TABLE dbo.IndexItems ADD CONSTRAINT
--	FK_IndexItems_ContentItems FOREIGN KEY
--	(
--	ContentItemID
--	) REFERENCES dbo.ContentItems
--	(
--	ID
--	) ON UPDATE  NO ACTION 
--	 ON DELETE  NO ACTION 
--	
--GO
COMMIT