CREATE PROCEDURE [dbo].[GetProductById]
	@Id int
AS
	SELECT * FROM Products WHERE [Id]=@Id
GO
