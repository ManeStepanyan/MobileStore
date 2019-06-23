CREATE PROCEDURE [dbo].[AddSeller]
	@Name varchar(30),
	@Address varchar(60),
	@CellPhone varchar(30),
	@Login varchar(40),
	@Password varchar(50),
	@Email varchar(40)
AS
	begin
    declare @userId int
    execute @userId = AddUser @Login, @Password, @Email, 2,0
	Insert into dbo.Sellers([UserId],[Name],[Address],[CellPhone])
	Values(@userId,@Name,@Address,@CellPhone)
	end
