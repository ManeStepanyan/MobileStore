CREATE PROCEDURE [dbo].[CreateUser]
    @Name varchar(30),
	@Surname varchar(30)=null,
	@Address varchar(60)=null,
	@CellPhone varchar(30)=null,
    @Login varchar(40),
	@Password varchar(50),
	@Email varchar(40),
	@Role_Id int
AS
begin
declare @userid int
	Execute @userid= AddUser @Login, @Password, @Email, @Role_Id
	if @Role_Id=1 Insert into dbo.Admins([User_Id],[Name])
	Values(@userid,@Name)
	if @Role_Id=2 Insert into dbo.Sellers([User_Id],[Name],[Address],[CellPhone])
	Values(@userid,@Name,@Address,@CellPhone)
	else 	Insert into dbo.Customers([User_Id],[Name],[Surname],[Status])
	Values(@userid,@Name,@Surname,0)
end
