CREATE PROCEDURE [dbo].[AddUser]
	@Login varchar(40),
	@Password varchar(50),
	@Email varchar(40),
	@Role_Id int
AS
	Insert into dbo.Users Values(@Login,@Password,@Email,@Role_Id)
	return scope_identity()
