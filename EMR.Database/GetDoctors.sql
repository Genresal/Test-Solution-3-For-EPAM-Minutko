CREATE PROCEDURE [dbo].[GetDoctors] @COLUMN NVARCHAR(30), @OPERATOR NVARCHAR(30), @VALUE NVARCHAR(30)

AS

DECLARE @CONDITION NVARCHAR(128) = ''
DECLARE @SQL_QUERY NVARCHAR (MAX)
IF (@COLUMN is not null)
BEGIN
SET @CONDITION = 'WHERE ' + @COLUMN + ' ' + @OPERATOR + ' ' + @VALUE
END
SET @SQL_QUERY = 'SELECT doc.Id
,UserId
,PositionId
,userDoc.Login as DoctorLogin
,userDoc.Password as DoctorPassword
,userDoc.FirstName as DoctorFirstName
,userDoc.LastName as DoctorLastName
,userDoc.RoleId as DoctorRoleId
,userDoc.Birthday as DoctorBirthday
,userDoc.Email as DoctorEmail
,userDoc.PhoneNumber as DoctorPhoneNumber
,userDoc.PhotoUrl as DoctorPhotoUrl
,docPos.Name as PositionName 
FROM [tDoctor] as doc 
LEFT JOIN tUser as userDoc ON userDoc.Id = doc.UserId 
LEFT JOIN tPosition as docPos ON docPos.Id = doc.PositionId ' + @CONDITION

			EXECUTE sp_executesql @SQL_QUERY
RETURN 0
