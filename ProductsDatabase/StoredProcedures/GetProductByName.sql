CREATE PROCEDURE [dbo].[GetProductByName]
	@Name VARCHAR(30)
AS
	SELECT * FROM Products WHERE [Name]=@Name
GO

