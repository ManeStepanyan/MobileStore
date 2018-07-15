CREATE PROCEDURE [dbo].[AddAdmin]
	@Name varchar(30),
	@Login varchar(40),
	@Password varchar(50),
	@Email varchar(40)

AS
	begin
    declare @userId int
    execute @userId = AddUser @Login, @Password, @Email, 1,0
	Insert into dbo.Admins([UserId],[Name])
	Values(@userId,@Name)
	end
