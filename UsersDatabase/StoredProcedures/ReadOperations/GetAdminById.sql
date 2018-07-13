CREATE PROCEDURE [dbo].[GetAdminById]
@Id int
AS
	SELECT *
	from Admins inner join Users on
	Admins.[User_Id]=Users.Id
	where Admins.Id=@Id
