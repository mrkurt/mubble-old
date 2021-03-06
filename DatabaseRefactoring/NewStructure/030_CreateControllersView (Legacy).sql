/****** Object:  View [dbo].[v_LegacyControllers]    Script Date: 01/14/2008 00:38:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_LegacyControllers]
AS
SELECT     dbo.ContentItems.ID, dbo.ContentItems.ContentItemID AS ControllerID, dbo.ContentItems.FileName, case when dbo.ContentItems.Path = '/' then '/index' else dbo.ContentItems.Path end as Path, dbo.ContentItems.PublishDate, 
                      dbo.ContentItems.Status, dbo.ContentTypes.ActiveObjectType, dbo.Controllers.TemplateID, dbo.Controllers.TemplateControl, dbo.Controllers.ModuleControlID, 
                      dbo.Controllers.ModuleControlView, dbo.Controllers.RouteID, dbo.Controllers.IsContent, dbo.Controllers.IsContentContainer, dbo.Controllers.Settings, 
                      dbo.TextBlocks.Title, dbo.TextBlocks.Excerpt, dbo.TextBlocks.Body
FROM         dbo.ContentItems INNER JOIN
                      dbo.ContentTypes ON dbo.ContentItems.ContentTypeID = dbo.ContentTypes.ID INNER JOIN
                      dbo.Controllers ON dbo.ContentItems.ID = dbo.Controllers.ContentItemID INNER JOIN
                      dbo.TextBlocks ON dbo.ContentItems.TextBlockID = dbo.TextBlocks.ID

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ContentItems"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 204
               Right = 190
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ContentTypes"
            Begin Extent = 
               Top = 6
               Left = 239
               Bottom = 215
               Right = 414
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Controllers"
            Begin Extent = 
               Top = 6
               Left = 452
               Bottom = 248
               Right = 636
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TextBlocks"
            Begin Extent = 
               Top = 204
               Left = 38
               Bottom = 321
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1710
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_LegacyControllers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_LegacyControllers'

GO

CREATE TRIGGER [dbo].[LegacyControllerInsert] 
   ON  [dbo].[v_LegacyControllers] 
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
		select a.ID, ControllerID, FileName, Path, PublishDate, Status, b.ID, a.ID
		from #inserted a, ContentTypes b where a.ActiveObjectType=b.ActiveObjectType

	insert into Controllers(
		ContentItemID, TemplateID, TemplateControl, ModuleControlID, ModuleControlView, RouteID, IsContent, IsContentContainer, Settings)
		select ID, TemplateID, TemplateControl, ModuleControlID, ModuleControlView, RouteID, IsContent, IsContentContainer, Settings
		from #inserted
END

GO

CREATE TRIGGER [dbo].[LegacyControllerUpdate] 
   ON  [dbo].[v_LegacyControllers] 
   INSTEAD OF UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	update a set Excerpt = b.Excerpt, Title = b.Title, Body = b.Body
	from TextBlocks a, inserted b, ContentItems c where b.ID=c.ID and 
		a.ID=c.TextBlockID and
		c.TextBlockID=a.ID

	update a set ContentItemID=b.ControllerID, FileName=b.FileName, PublishDate=b.PublishDate,
		Status = b.Status, ContentTypeID=c.ID
	from ContentItems a, inserted b, ContentTypes c where a.ID=b.ID and c.ActiveObjectType = b.ActiveObjectType

	update a set TemplateID = b.TemplateID, TemplateControl = b.TemplateControl, ModuleControlID = b.ModuleControlID, 
		ModuleControlView = b.ModuleControlView, RouteID = b.RouteID, IsContent = b.IsContent, 
		IsContentContainer = b.IsContentContainer, Settings = b.Settings
	from Controllers a, inserted b where a.ContentItemID = b.ID
END

GO

CREATE TRIGGER [dbo].[LegacyControllerDelete] 
   ON  [dbo].[v_LegacyControllers] 
   INSTEAD OF DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	delete from Controllers where ContentItemID in (SELECT ID FROM deleted)

	delete from ContentItems where ID in (Select ID FROM deleted)
END

GO