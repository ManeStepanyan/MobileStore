CREATE PROCEDURE [dbo].[DeleteProduct]
	@Id int
AS
	delete from Products where [Id]=@Id
GO