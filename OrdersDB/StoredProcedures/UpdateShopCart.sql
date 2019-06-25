CREATE PROCEDURE [dbo].[UpdateShopCart]
	@Id int,
	@Quantity int
AS
	UPDATE ShopCart
	SET Quantity=@Quantity
	WHERE [Id]=@Id;
GO

