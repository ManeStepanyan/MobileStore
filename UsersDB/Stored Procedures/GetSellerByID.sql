CREATE PROCEDURE [dbo].[GetSellerByID]
	@Id int
AS
	SELECT * from Sellers
	where Id= @Id
Go
