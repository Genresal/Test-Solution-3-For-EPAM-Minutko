CREATE PROCEDURE [dbo].[CreateRecord] @DiagnosisName NVARCHAR(255)
                                    , @DoctorId INT
                                    , @PatientId INT
                                    , @ModifiedDate DATETIME

AS

BEGIN TRY 
     BEGIN TRAN 

          INSERT INTO [dbo].[tDiagnosis]
          ([Name])
          VALUES
          (@DiagnosisName)

          INSERT INTO [dbo].[tRecord]
          ([DiagnosisId]
          ,[DoctorId]
          ,[PatientId]
          ,[ModifiedDate])
          VALUES
          (SCOPE_IDENTITY()
          ,@DoctorId
          ,@PatientId
          ,@ModifiedDate)

      COMMIT TRAN
    END TRY
    BEGIN CATCH
      ROLLBACK TRAN
    END CATCH
