CREATE PROCEDURE [dbo].[GetProducts]
	@Seller_Id int
AS
	SELECT Product_Id from [Catalog] where Seller_Id=@Seller_Id
GO
