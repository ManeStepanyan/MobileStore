CREATE PROCEDURE [dbo].[DeleteCustomerProduct]
	@Product_Id int
AS
	delete from CustomerProduct 
	where Product_Id=@Product_Id
GO
