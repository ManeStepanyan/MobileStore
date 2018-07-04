CREATE PROCEDURE [dbo].[DeleteCustomer]
	@Id int
AS
	Delete from Customers
	where Id=@Id
RETURN 0
