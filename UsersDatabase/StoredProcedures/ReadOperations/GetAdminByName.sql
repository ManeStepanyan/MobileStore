CREATE PROCEDURE [dbo].[GetAdminByName]
@Login varchar(40)
AS
	SELECT *
	from Admins inner join Users on
	Admins.[UserId]=Users.Id
	where Users.[Login]=@Login
