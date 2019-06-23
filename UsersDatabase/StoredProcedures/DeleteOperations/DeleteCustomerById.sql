CREATE PROCEDURE [dbo].[DeleteCustomer]
@Id int
AS
begin
declare @userId int
select @userId= [UserId] from Customers where Id=@Id
delete from Customers where Id=@Id
delete from Users where Id=@userId
end

