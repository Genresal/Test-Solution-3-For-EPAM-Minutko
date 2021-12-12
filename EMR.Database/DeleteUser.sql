﻿CREATE PROCEDURE [dbo].[DeleteUser] @Id INT

AS

BEGIN TRY 
BEGIN TRAN 

	DELETE FROM [dbo].[tDiagnosis] WHERE Id IN
		(SELECT DiagnosisId FROM [tRecord] WHERE PatientId IN 
			(SELECT Id FROM [tPatient] WHERE UserId = @Id)) 

	DELETE FROM [dbo].[tUser] 
	WHERE Id = @Id

COMMIT TRAN 
END TRY 
BEGIN CATCH 
ROLLBACK TRAN 
END CATCH

RETURN 0
