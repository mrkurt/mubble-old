
/****** Object:  StoredProcedure [dbo].[qd_aspnet_Membership_GetPasswordAnswer]    Script Date: 04/18/2006 15:40:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[qd_aspnet_Membership_GetPasswordAnswer]
    @UserId uniqueidentifier
AS
SELECT PasswordAnswer FROM dbo.aspnet_Membership WHERE UserID=@UserID 


/****** Object:  StoredProcedure [dbo].[qd_aspnet_Membership_ChangeUserName]    Script Date: 04/18/2006 15:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[qd_aspnet_Membership_ChangeUserName]
    @ApplicationName                        nvarchar(256),
    @UserId                                 uniqueidentifier,
    @NewUserName                            nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ExistingUserId uniqueidentifier
    SELECT @ExistingUserId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END


    SELECT  @ExistingUserId = UserId FROM dbo.aspnet_Users WITH ( UPDLOCK, HOLDLOCK )
		WHERE LOWER(@NewUserName) = LoweredUserName AND @ApplicationId = ApplicationId
    
	IF ( @ExistingUserID IS NULL)
		BEGIN
			/* userName is available */
			UPDATE dbo.aspnet_Users SET UserName = @NewUserName, LoweredUserName= LOWER(@NewUserName) WHERE userID=@UserID
		END
    ELSE
		BEGIN
			IF( @ExistingUserID = @UserId)
				BEGIN
					SET @ErrorCode = 1
					GOTO Cleanup
				END
			ELSE
				BEGIN
					SET @ErrorCode = 2
					GOTO Cleanup

				END
		END

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
