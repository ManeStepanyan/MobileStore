CREATE PROCEDURE [dbo].[AddCustomerProduct]
	@Product_Id int,
	@Customer_Id int
AS
	insert into CustomerProduct(Product_Id, Customer_Id)
	values (@Product_Id, @Customer_Id)
GO
