CREATE PROCEDURE [dbo].[DeleteRecord] @Id INT

AS

BEGIN TRY 
BEGIN TRAN 
	DELETE FROM [dbo].[tDrug] 
	WHERE Id IN (SELECT DrugId FROM tRecordTreatment WHERE RecordId = @Id) 
	DELETE FROM [dbo].[tProcedure] 
	WHERE Id IN (SELECT ProcedureId FROM tRecordTreatment WHERE RecordId = @Id) 
	DELETE FROM [dbo].[tDiagnosis] WHERE Id in 
		(SELECT DiagnosisId FROM tRecord WHERE Id = @Id) 
COMMIT TRAN 
END TRY 
BEGIN CATCH 
ROLLBACK TRAN 
END CATCH

RETURN 0
