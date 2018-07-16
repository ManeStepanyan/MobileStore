CREATE PROCEDURE [dbo].[GetOrderByProductId]
	@Id int
AS
	select * from Orders where Product_ID=@Id	
GO
