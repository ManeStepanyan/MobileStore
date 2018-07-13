CREATE PROCEDURE [dbo].[DeleteUser]
	@Id int
AS
begin
declare @roleid int
	select @roleid= Role_Id from Users where Id=@Id
	if @roleid=1 delete from Admins where [User_Id]=@Id
	if @roleid=2 delete from Sellers where [User_Id]=@Id
	else delete from Customers where [User_Id]=@Id
delete from Users where Id=@Id
end
