CREATE PROCEDURE [dbo].[DeleteSeller]
	@Id int
AS
	Delete from Sellers
	where Id=@Id
Go
