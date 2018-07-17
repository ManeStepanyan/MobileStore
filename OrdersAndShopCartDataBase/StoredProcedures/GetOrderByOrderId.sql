CREATE PROCEDURE [dbo].[GetOrderByOrderId]
	@Id int
AS
	select * from Orders where [Id]=@Id
GO