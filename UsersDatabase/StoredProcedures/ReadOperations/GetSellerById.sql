CREATE PROCEDURE [dbo].[GetSellerById]
	@Id int
AS
	SELECT *
	from Sellers inner join Users on
	Sellers.[User_Id]=Users.Id
	where Sellers.Id=@Id
