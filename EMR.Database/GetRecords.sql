CREATE PROCEDURE [dbo].[GetRecords] @COLUMN NVARCHAR(30), @OPERATOR NVARCHAR(30), @VALUE NVARCHAR(30)

AS

DECLARE @CONDITION NVARCHAR(128) = ''
DECLARE @SQL_QUERY NVARCHAR (MAX)
IF (@COLUMN is not null)
BEGIN
SET @CONDITION = 'WHERE ' + @COLUMN + ' ' + @OPERATOR + ' ''' + @VALUE + ''''
END
SET @SQL_QUERY = 'SELECT rec.Id
			,DiagnosisId
			,SickLeaveId
			,DoctorId
			,PatientId
			,ModifiedDate
			,diagnosis.Name as DiagnosisName
			,doc.PositionId as DoctorPositionId
			,doc.UserId as DoctorUserId
			,userDoc.FirstName as DoctorFirstName
			,userDoc.LastName as DoctorLastName
			,userDoc.Birthday as DoctorBirthday
			,userDoc.Email as DoctorEmail
			,userDoc.PhoneNumber as DoctorPhoneNumber
			,userDoc.PhotoUrl as DoctorPhotoUrl
			,pat.Job as PatientJob
			,pat.UserId as PatientUserId
			,userPat.FirstName as PatientFirstName
			,userPat.LastName as PatientLastName
			,userPat.Birthday as PatientBirthday
			,userPat.Email as PatientEmail
			,userPat.PhoneNumber as PatientPhoneNumber
			,userPat.PhotoUrl as PatientPhotoUrl
			,pos.Name as DoctorName
			,sick.Id as SickLeaveId
			,sick.Number as SickLeaveNumber
			,sick.StartDate as SickLeaveStartDate
			,sick.FinalDate as SickLeaveFinalDate 
			FROM tRecord as rec 
			LEFT JOIN tDoctor as doc ON doc.Id = rec.DoctorId 
			LEFT JOIN tPatient as pat ON pat.Id = rec.PatientId 
			LEFT JOIN tUser as userDoc ON userDoc.Id = doc.UserId 
			LEFT JOIN tUser as userPat ON userPat.Id = pat.UserId 
			LEFT JOIN tDiagnosis as diagnosis ON diagnosis.Id = rec.DiagnosisId 
			LEFT JOIN tPosition as pos ON pos.Id = doc.PositionId 
			LEFT JOIN tSickLeave as sick ON sick.Id = rec.SickLeaveId ' + @CONDITION

			EXECUTE sp_executesql @SQL_QUERY
RETURN 0
