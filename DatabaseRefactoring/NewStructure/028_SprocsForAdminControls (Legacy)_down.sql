/****** Object:  StoredProcedure [dbo].[admincontrols_Save]    Script Date: 01/13/2008 23:02:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[admincontrols_Save]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[admincontrols_Save]

/****** Object:  StoredProcedure [dbo].[admincontrols_List]    Script Date: 01/13/2008 23:02:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[admincontrols_List]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[admincontrols_List]

/****** Object:  StoredProcedure [dbo].[admincontrols_Delete]    Script Date: 01/13/2008 23:02:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[admincontrols_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[admincontrols_Delete]