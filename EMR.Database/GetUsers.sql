CREATE PROCEDURE [dbo].[GetUsers] @COLUMN NVARCHAR(30), @OPERATOR NVARCHAR(30), @VALUE NVARCHAR(30)

AS

DECLARE @CONDITION NVARCHAR(128) = ''
DECLARE @SQL_QUERY NVARCHAR (MAX)
IF (@COLUMN is not null)
BEGIN
SET @CONDITION = 'WHERE ' + @COLUMN + ' ' + @OPERATOR + ' ''' + @VALUE + ''''
END
SET @SQL_QUERY = 'SELECT u.Id
,Login
,Password
,FirstName
,LastName
,RoleId
,Birthday
,Email
,PhoneNumber
,PhotoUrl
,r.Name as RoleName
FROM [tUser] as u 
LEFT JOIN [tRole] as r ON r.Id = r.RoleId ' + @CONDITION

EXECUTE sp_executesql @SQL_QUERY
RETURN 0
