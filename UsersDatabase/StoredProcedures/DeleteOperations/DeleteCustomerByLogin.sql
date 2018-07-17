CREATE PROCEDURE [dbo].[DeleteCustomerByLogin]
	@Login varchar(40)
AS
begin
declare @userId int
select	@userId= Id from Users where [Login]=@Login
delete from Customers where [UserId]=@userId
delete from Users where [Login]=@Login
end
