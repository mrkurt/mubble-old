-- =============================================
-- Author:		Kurt Mackey
-- Create date: 12/27/2007
-- Description:	Updates paths on changed items and children
-- =============================================
CREATE TRIGGER [dbo].[SyncChangedPaths] 
   ON  [dbo].[ContentItems] 
   AFTER Insert, Update
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--IF EXISTS(SELECT a.ID FROM Inserted a, Deleted b WHERE a.ID=b.ID and a.FileName <> b.FileName

	IF EXISTS(SELECT a.ID FROM Inserted a 
	LEFT OUTER JOIN Deleted b ON(a.ID = b.ID)
	WHERE b.FileName IS NULL OR a.FileName <> b.FileName)
	
	BEGIN

		WITH ContentTree(ID, Path) AS
		(
			SELECT ID, cast('/' as nvarchar(4000)) as Path
			FROM ContentItems WHERE ContentItemID IS NULL

			UNION ALL

			SELECT a.ID, 
				CASE WHEN b.Path <> '/' THEN
					b.Path + '/' + a.FileName
				ELSE
					'/' + a.FileName
				End
			FROM ContentItems a INNER JOIN ContentTree b ON a.ContentItemID = b.ID
		)

		UPDATE ContentItems SET Path = a.Path
		FROM ContentTree a 
		LEFT OUTER JOIN ContentItems b ON (a.ID = b.ID)
		WHERE a.Path <> b.Path OR b.Path IS NULL

	END
END

