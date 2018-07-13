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
	if @Role_Id=1 insert into dbo.Admins Values(@userid,@Name)
	if @Role_Id=2 insert into dbo.Sellers Values(@userid,@Name,@Address,@CellPhone)
	else insert into dbo.Customers Values(@userid,@Name,@Surname)
end
