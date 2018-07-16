CREATE PROCEDURE [dbo].[AddToShopCart]
	@Customer_Id int,
	@Product_Id int
AS
	Insert into ShopCart(Customer_Id, Product_Id)
	values(@Customer_Id, @Product_Id)
GO
