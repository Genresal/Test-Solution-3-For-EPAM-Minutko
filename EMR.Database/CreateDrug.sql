CREATE PROCEDURE [dbo].[CreateDrug] @RecordId INT
                                    , @Name NVARCHAR(60)
                                    , @Description NVARCHAR(128)

AS

BEGIN TRY 
     BEGIN TRAN 

        INSERT INTO [dbo].[tDrug]
        ([Name]
        ,[Description])
        VALUES
        (@Name
        ,@Description)
        INSERT INTO [dbo].[tRecordTreatment]
        ([RecordId]
        ,[DrugId])
        VALUES
        (@RecordId
        ,SCOPE_IDENTITY())

      COMMIT TRAN
    END TRY
    BEGIN CATCH
      ROLLBACK TRAN
    END CATCH

RETURN 0
