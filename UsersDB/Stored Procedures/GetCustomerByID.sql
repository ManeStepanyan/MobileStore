CREATE PROCEDURE [dbo].[GetCustomerByID]
	@Id int
AS
	select * from Customers where Id= @Id
Go
