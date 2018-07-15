CREATE PROCEDURE [dbo].[AddUser]
	@Login varchar(40),
	@Password varchar(50),
	@Email varchar(40),
	@Role_Id int,
	@IsValidated bit
AS
	Insert into dbo.Users Values(@Login,@Password,@Email,@Role_Id,@IsValidated)
	return scope_identity()
