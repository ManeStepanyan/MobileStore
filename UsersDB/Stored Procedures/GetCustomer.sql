CREATE PROCEDURE [dbo].[GetCustomer]
	@Id int
AS
	Select * from Customers
	where Id=@Id
GO