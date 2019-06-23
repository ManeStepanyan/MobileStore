CREATE PROCEDURE [dbo].[GetAdmins]
AS
	SELECT *
	from Admins inner join Users
	on Admins.[UserId]=Users.Id
