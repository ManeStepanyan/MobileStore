CREATE PROCEDURE [dbo].[GetOrderByProductId]
	@Product_Id int
AS
	select * from Orders where Product_Id=@Product_Id
GO