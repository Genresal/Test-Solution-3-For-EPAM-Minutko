CREATE PROCEDURE [dbo].[CreateDoctor] @Login NVARCHAR(40)
                                    , @Password NVARCHAR(40)
                                    , @RoleId int
                                    , @FirstName NVARCHAR(60)
                                    , @LastName NVARCHAR(60)
                                    , @BirthDay DATETIME
                                    , @Email NVARCHAR(128)
                                    , @PhoneNumber NVARCHAR(30)
                                    , @PhotoUrl NVARCHAR(255)
                                    , @PositionId INT

AS

BEGIN TRY 
     BEGIN TRAN 
          INSERT INTO [dbo].[tUser]
          ([Login]
          ,[Password]
          ,[RoleId]
          ,[FirstName]
          ,[LastName]
          ,[Birthday]
          ,[Email]
          ,[PhoneNumber]
          ,[PhotoUrl])
          VALUES
          (@Login
          ,@Password
          ,@RoleId
          ,@FirstName
          ,@LastName
          ,@Birthday
          ,@Email
          ,@PhoneNumber
          ,@PhotoUrl)
    
          INSERT INTO [dbo].[tDoctor]
          ([UserId]
          ,[PositionId])
          VALUES
          (SCOPE_IDENTITY()
          ,@PositionId)
      COMMIT TRAN
    END TRY
    BEGIN CATCH
      ROLLBACK TRAN
    END CATCH

RETURN 0
