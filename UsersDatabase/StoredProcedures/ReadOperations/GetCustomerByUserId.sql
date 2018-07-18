CREATE PROCEDURE [dbo].[GetCustomerByUserId]
	@userid int
AS
begin
	SELECT * from Customers
	inner join Users on Customers.UserId=Users.Id
	where Customers.UserId=@userid
end
