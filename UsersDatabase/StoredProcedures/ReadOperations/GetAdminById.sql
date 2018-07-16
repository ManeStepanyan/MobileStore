CREATE PROCEDURE [dbo].[GetAdminById]
@Id int
AS
	SELECT *
	from Admins inner join Users on
	Admins.[UserId]=Users.Id
	where Admins.Id=@Id
