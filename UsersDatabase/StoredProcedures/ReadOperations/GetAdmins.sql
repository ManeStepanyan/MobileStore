CREATE PROCEDURE [dbo].[GetAdmins]
AS
	SELECT *
	from Admins inner join Users
	on Admins.[User_Id]=Users.Id
