﻿CREATE PROCEDURE [dbo].[DeleteSickLeave] @Id INT

AS

BEGIN TRY 
BEGIN TRAN 

UPDATE [dbo].[tRecord]
SET [SickLeaveId] = null
WHERE SickLeaveId = @Id

DELETE FROM [dbo].[tSickLeave]
WHERE Id = @Id

COMMIT TRAN 
END TRY 
BEGIN CATCH 
ROLLBACK TRAN 
END CATCH

RETURN 0
