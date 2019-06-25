CREATE PROCEDURE [dbo].[AddSellerProduct]
	@Product_Id int,
	@Seller_Id int
AS
	insert into SellerProduct(Product_Id, Seller_Id)
	values (@Product_Id, @Seller_Id)
GO

