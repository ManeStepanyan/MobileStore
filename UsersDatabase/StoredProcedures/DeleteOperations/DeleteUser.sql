CREATE PROCEDURE [dbo].[DeleteUser]
	@Id int
AS
begin
declare @roleid int
	select @roleid= RoleId from Users where Id=@Id
	if @roleid=1 delete from Admins where [UserId]=@Id
	if @roleid=2 delete from Sellers where [UserId]=@Id
	else delete from Customers where [UserId]=@Id
delete from Users where Id=@Id
end
