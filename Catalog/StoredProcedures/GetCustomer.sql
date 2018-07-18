CREATE PROCEDURE [dbo].[GetCustomer]
	@Product_Id int
AS
	select * from CustomerProduct where Product_Id=@Product_Id
GO
