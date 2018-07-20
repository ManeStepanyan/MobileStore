CREATE PROCEDURE [dbo].[AddToShopCart]
	@Customer_Id int,
	@Product_Id int, 
	@Quantity int
AS
	Insert into ShopCart(Customer_Id, Product_Id, Quantity)
	values(@Customer_Id, @Product_Id, @Quantity)
GO
