CREATE PROCEDURE [dbo].[GetProductByCustomerId]
	@Customer_Id int
AS
	select * from CustomerProduct where Customer_Id=@Customer_Id
GO
