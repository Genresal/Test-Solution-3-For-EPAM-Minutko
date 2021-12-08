CREATE PROCEDURE [dbo].[GetPatients] @COLUMN NVARCHAR(30), @OPERATOR NVARCHAR(30), @VALUE NVARCHAR(30)

AS

DECLARE @CONDITION NVARCHAR(128) = ''
DECLARE @SQL_QUERY NVARCHAR (MAX)
IF (@COLUMN is not null)
BEGIN
SET @CONDITION = 'WHERE ' + @COLUMN + ' ' + @OPERATOR + ' ''' + @VALUE + ''''
END
SET @SQL_QUERY = 'SELECT pat.Id
,UserId
,Job
,userPat.Login as PatientLogin
,userPat.Password as PatientPassword
,userPat.FirstName as PatientFirstName
,userPat.LastName as PatientLastName
,userPat.RoleId as PatientRoleId
,userPat.Birthday as PatientBirthday
,userPat.Email as PatientEmail
,userPat.PhoneNumber as PatientPhoneNumber
,userPat.PhotoUrl as PatientPhotoUrl
FROM [tPatient] as pat 
LEFT JOIN tUser as userPat ON userPat.Id = pat.UserId ' + @CONDITION

EXECUTE sp_executesql @SQL_QUERY
RETURN 0
