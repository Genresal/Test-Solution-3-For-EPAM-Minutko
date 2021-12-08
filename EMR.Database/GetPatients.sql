CREATE PROCEDURE [dbo].[GetPatients] @COLUMN NVARCHAR(30), @OPERATOR NVARCHAR(30), @VALUE NVARCHAR(30)

AS

DECLARE @CONDITION NVARCHAR(128) = ''
DECLARE @SQL_QUERY NVARCHAR (MAX)
IF (@COLUMN is not null)
BEGIN
SET @CONDITION = 'WHERE ' + @COLUMN + ' ' + @OPERATOR + ' ' + @VALUE
END
SET @SQL_QUERY = 'SELECT pat.Id
,UserId
,Job
,userPat.Login as DoctorLogin
,userPat.Password as DoctorPassword
,userPat.FirstName as DoctorFirstName
,userPat.LastName as DoctorLastName
,userPat.RoleId as DoctorRoleId
,userPat.Birthday as DoctorBirthday
,userPat.Email as DoctorEmail
,userPat.PhoneNumber as DoctorPhoneNumber
,userPat.PhotoUrl as DoctorPhotoUrl
FROM [tPatient] as pat 
LEFT JOIN tUser as userPat ON userPat.Id = pat.UserId ' + @CONDITION

EXECUTE sp_executesql @SQL_QUERY
RETURN 0
