CREATE PROCEDURE [dbo].[DeleteOrder]
	@Id int
AS
	delete from Orders
	where [Id]=@Id
GO