CREATE PROCEDURE [dbo].[CreateSickLeave] @Number NVARCHAR(60)
                                    , @StartDate DATETIME
                                    , @FinalDate DATETIME
                                    , @Recordid int

AS

BEGIN TRY
BEGIN TRAN

INSERT INTO [dbo].[tSickLeave]
([Number]
,[StartDate]
,[FinalDate])
VALUES
(@Number
,@StartDate
,@FinalDate)
UPDATE [dbo].[tRecord]
SET [SickLeaveId] = SCOPE_IDENTITY()
WHERE Id = @RecordId

COMMIT TRAN
END TRY
BEGIN CATCH
ROLLBACK TRAN
END CATCH

RETURN 0
