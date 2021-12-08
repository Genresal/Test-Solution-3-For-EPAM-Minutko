﻿CREATE PROCEDURE [dbo].[DeletePatient] @Id INT

AS

BEGIN TRY 
BEGIN TRAN 

	DELETE FROM [dbo].[tDiagnosis] WHERE Id in 
		(SELECT DiagnosisId FROM [tRecord] WHERE PatientId = @Id) 

	DELETE FROM [dbo].[tUser] 
	WHERE Id IN (SELECT UserId FROM [tPatient] WHERE Id = @Id) 

COMMIT TRAN 
END TRY 
BEGIN CATCH 
ROLLBACK TRAN 
END CATCH

RETURN 0