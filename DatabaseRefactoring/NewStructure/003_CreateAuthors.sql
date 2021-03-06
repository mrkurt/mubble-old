/*
   Friday, December 21, 200712:59:39 PM
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
CREATE TABLE dbo.Authors
	(
	ID uniqueidentifier NOT NULL,
	UserName nvarchar(255) NOT NULL,
	Email nvarchar(255) NULL,
	Displayname nvarchar(255) NOT NULL,
	Bio nvarchar(MAX) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Authors ADD CONSTRAINT
	DF_Authors_ID DEFAULT newsequentialid() FOR ID
	
ALTER TABLE dbo.Authors ADD CONSTRAINT
	PK_Authors PRIMARY KEY NONCLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.Authors', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Authors', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Authors', 'Object', 'CONTROL') as Contr_Per 