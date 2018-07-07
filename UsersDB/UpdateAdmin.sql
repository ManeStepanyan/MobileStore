CREATE PROCEDURE [dbo].[UpdateAdmin]
@Id int,
	@Name varchar(20),
	@Login varchar(40),
	@Password varchar(30)
AS
	Update Admins
	set [Name]=@Name, [Login]=@Login, [Password]=@Password
	where Id=@Id
