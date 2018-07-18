CREATE PROCEDURE [dbo].[GetCustomerByUserId]
	@userid int
AS
begin
	SELECT * from Customers where UserId=@userid
end
