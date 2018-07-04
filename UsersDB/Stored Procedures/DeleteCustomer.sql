CREATE PROCEDURE [dbo].[DeleteCustomer]
	@Id int
AS
	Delete from Customers
	where Id=@Id
GO
