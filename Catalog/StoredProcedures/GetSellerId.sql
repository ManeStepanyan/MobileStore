CREATE PROCEDURE [dbo].[GetSellerId]
	@Product_Id int
AS
	select Seller_Id from [Catalog] where Product_Id=@Product_Id
GO