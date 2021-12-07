CREATE PROCEDURE [dbo].[DeleteRecord] @Id INT

AS

BEGIN TRY 
BEGIN TRAN 
DECLARE @Id int = 199
DELETE FROM [dbo].[tDrug] 
WHERE Id in (Select DrugId from tRecordTreatment where RecordId = @Id) 
DELETE FROM [dbo].[tProcedure] 
WHERE Id in (Select ProcedureId from tRecordTreatment where RecordId = @Id) 
DELETE FROM [dbo].[tDiagnosis] WHERE Id in (Select DiagnosisId from tRecord where Id = @Id) 
COMMIT TRAN 
END TRY 
BEGIN CATCH 
ROLLBACK TRAN 
END CATCH

RETURN 0
