CREATE PROCEDURE [dbo].[GetAdminByName]
@Login varchar(40)
AS
	SELECT *
	from Admins inner join Users on
	Admins.[User_Id]=Users.Id
	where Users.[Login]=@Login
