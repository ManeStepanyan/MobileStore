CREATE PROCEDURE [dbo].[GetSellerByName]
	@Login varchar(40)
AS
	SELECT * from Sellers where [Login]=@Login
Go