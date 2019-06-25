CREATE PROCEDURE [dbo].[DeleteSellerProduct]
	@Product_Id int
AS
	delete from SellerProduct 
	where Product_Id=@Product_Id
GO
