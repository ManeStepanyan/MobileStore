CREATE PROCEDURE [dbo].[DeleteCustomer]
@Id int
AS
begin
declare @userId int
select @userId= [User_Id] from Customers where Id=@Id
delete from Customers where Id=@Id
delete from Users where Id=@userId
end

