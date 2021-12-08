CREATE PROCEDURE [dbo].[GetPatientsInfo] @doctorId INT

AS

SELECT [PatientId]
,COUNT(t.Id) as RecordsNumber
,MAX([ModifiedDate]) as LastRecordModified
,(SELECT [FirstName] FROM [tUser] WHERE [Id] = MAX(p.UserId)) as FirstName
,(SELECT [LastName] FROM [tUser] WHERE [Id] = MAX(p.UserId)) as LastName 
FROM [tRecord] as t 
LEFT JOIN [tPatient] as p on p.Id = t.PatientId WHERE [DoctorId] = @doctorId 
GROUP BY PatientId

RETURN 0
