CREATE PROCEDURE [dbo].[UpdatePatient] @Login NVARCHAR(40)
                                    , @Password NVARCHAR(40)
                                    , @RoleId int
                                    , @FirstName NVARCHAR(60)
                                    , @LastName NVARCHAR(60)
                                    , @BirthDay DATETIME
                                    , @Email NVARCHAR(128)
                                    , @PhoneNumber NVARCHAR(30)
                                    , @PhotoUrl NVARCHAR(255)
                                    , @Job NVARCHAR(255)
                                    , @Id INT
                                    , @UserId INT

AS

BEGIN TRY 
     BEGIN TRAN 
          UPDATE [dbo].[tUser]
          SET 
          [Login] = @Login
          ,[Password] = @Password
          ,[RoleId] = @RoleId
          ,[FirstName] = @FirstName
          ,[LastName] = @LastName
          ,[Birthday] = @Birthday
          ,[Email] = @Email
          ,[PhoneNumber] = @PhoneNumber
          ,[PhotoUrl] = @PhotoUrl
          WHERE Id = @UserId 
            
          UPDATE [dbo].[tPatient] 
          SET 
          [UserId] = @UserId
          ,[Job] = @Job
          WHERE Id = @Id 

      COMMIT TRAN
    END TRY
    BEGIN CATCH
      ROLLBACK TRAN
    END CATCH

RETURN 0
