CREATE PROCEDURE [dbo].[AddUser]
	@Login varchar(40),
	@Password varchar(50),
	@Email varchar(40),
	@Role_Id int,
	@IsValidated bit,
	@ActivationCode varchar(80)
AS
	Insert into dbo.Users Values(@Login,@Password,@Email,@Role_Id,@IsValidated,1,@ActivationCode)
	return scope_identity()
