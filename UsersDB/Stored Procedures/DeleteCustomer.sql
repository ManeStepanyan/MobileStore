CREATE PROCEDURE [dbo].[DeleteCustomer]
@Id int
AS
	delete from Customers where Id=@Id
Go
