CREATE PROCEDURE [dbo].[DeleteFromShopCart]
	@Id int
AS
	delete from ShopCart
	where [Id]=@Id 
GO
