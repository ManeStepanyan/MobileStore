CREATE PROCEDURE [dbo].[CreateUser]
    @Name varchar(30),
	@Surname varchar(30)=null,
	@Address varchar(60)=null,
	@CellPhone varchar(30)=null,
    @Login varchar(40),
	@Password varchar(50),
	@Email varchar(40),
	@Role_Id int,
	@ActivationCode varchar(80)
AS
begin
declare @userid int
	Execute @userid= AddUser @Login, @Password, @Email, @Role_Id,0,@ActivationCode
	if @Role_Id=1 Insert into dbo.Admins([UserId],[Name])
	Values(@userid,@Name)
	if @Role_Id=2 Insert into dbo.Sellers([UserId],[Name],[Address],[CellPhone])
	Values(@userid,@Name,@Address,@CellPhone)
	else 	Insert into dbo.Customers([UserId],[Name],[Surname])
	Values(@userid,@Name,@Surname)
end
