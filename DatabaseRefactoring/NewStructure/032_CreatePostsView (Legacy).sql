/****** Object:  View [dbo].[v_LegacyPosts]    Script Date: 01/15/2008 15:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_LegacyPosts]
AS
SELECT     dbo.ContentItems.ID, dbo.ContentItems.ContentItemID AS ControllerID, dbo.ContentItems.FileName AS Slug, dbo.ContentItems.PublishDate, dbo.ContentItems.Status, 
                      dbo.TextBlocks.Title, dbo.TextBlocks.Body, dbo.TextBlocks.Excerpt, dbo.Authors.UserName
FROM         dbo.ContentItems INNER JOIN
                      dbo.ContentTypes ON dbo.ContentItems.ContentTypeID = dbo.ContentTypes.ID INNER JOIN
                      dbo.TextBlocks ON dbo.ContentItems.TextBlockID = dbo.TextBlocks.ID INNER JOIN
                      dbo.ContentItemAuthors ON dbo.ContentItems.ID = dbo.ContentItemAuthors.ContentItemID INNER JOIN
                      dbo.Authors ON dbo.ContentItemAuthors.AuthorID = dbo.Authors.ID
WHERE     (dbo.ContentTypes.ActiveObjectType = 'Mubble.Models.Post, Mubble.Models')

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 8
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ContentItems"
            Begin Extent = 
               Top = 30
               Left = 216
               Bottom = 147
               Right = 379
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "ContentTypes"
            Begin Extent = 
               Top = 133
               Left = 626
               Bottom = 276
               Right = 815
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TextBlocks"
            Begin Extent = 
               Top = 13
               Left = 666
               Bottom = 130
               Right = 826
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Authors"
            Begin Extent = 
               Top = 289
               Left = 492
               Bottom = 406
               Right = 652
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ContentItemAuthors"
            Begin Extent = 
               Top = 316
               Left = 50
               Bottom = 403
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      PaneHidden = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
      ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_LegacyPosts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'   Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 3180
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_LegacyPosts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_LegacyPosts'
GO
CREATE TRIGGER [dbo].[LegacyPostInsert] 
   ON  [dbo].[v_LegacyPosts] 
   INSTEAD OF INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Handle inserts
	select * into #inserted from inserted

	update #inserted set id = newid() where id is null

	insert into TextBlocks(ID, Title, Excerpt, Body)
		select ID, Title, Excerpt, Body FROM #inserted

	insert into ContentItems(ID, ContentItemID, FileName, Path, PublishDate, Status, ContentTypeID, TextBlockID)
		select a.ID, ControllerID, Slug, CAST(a.ID as nvarchar(255)) as Path, PublishDate, Status, b.ID, a.ID
		from #inserted a, ContentTypes b where b.ActiveObjectType = 'Mubble.Models.Post, Mubble.Models.Post'

	insert into Authors(Username, DisplayName)
	select username, username from #inserted where username not in (select username from authors)

	insert into ContentItemAuthors 
	select a.id as ContentItemID, b.ID as AuthorID from #inserted a, Authors b
	where a.username = b.username
END
GO
USE [mubble]
GO
CREATE TRIGGER [dbo].[LegacyPostUpdate] 
   ON  [dbo].[v_LegacyPosts] 
   INSTEAD OF UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Handle inserts
	update a set Excerpt = b.Excerpt, Title = b.Title, Body = b.Body
	from TextBlocks a, inserted b, ContentItems c where b.ID=c.ID and 
		a.ID=c.TextBlockID and
		c.TextBlockID=a.ID

	update a set ContentItemID=b.ControllerID, FileName=b.Slug, PublishDate=b.PublishDate,
		Status = b.Status
		from ContentItems a, inserted b where a.ID=b.ID

	insert into Authors(Username, DisplayName)
	select username, username from inserted where username not in (select username from authors)

	delete from ContentItemAuthors where ContentItemID in (SELECT ID FROM inserted)

	insert into ContentItemAuthors 
		select a.id as ContentItemID, b.ID as AuthorID from #inserted a, Authors b
		where a.username = b.username
END
GO
CREATE TRIGGER [dbo].[LegacyPostDelete] 
   ON  [dbo].[v_LegacyPosts] 
   INSTEAD OF DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	delete from ContentItemAuthors where ContentItemID in (SELECT ID FROM deleted)

	delete from ContentItems where ID in (Select ID FROM deleted)
END