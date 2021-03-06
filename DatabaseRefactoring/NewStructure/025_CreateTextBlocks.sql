/*
   Friday, January 11, 20083:25:07 PM
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
CREATE TABLE dbo.TextBlocks
	(
	ID uniqueidentifier NOT NULL,
	Title nvarchar(255) NULL,
	Excerpt nvarchar(max) NULL,
	Body nvarchar(max) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.TextBlocks ADD CONSTRAINT
	DF_TextBlocks_ID DEFAULT newsequentialid() FOR ID
GO
ALTER TABLE dbo.TextBlocks ADD CONSTRAINT
	PK_TextBlocks PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
