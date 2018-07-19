CREATE PROCEDURE [dbo].[GetCustomerId]
	@Product_Id int
AS
	select * from CustomerProduct where Product_Id=@Product_Id
GO
