CREATE PROCEDURE [dbo].[GetSellerById]
	@Id int
AS
	SELECT *
	from Sellers inner join Users on
	Sellers.[UserId]=Users.Id
	where Sellers.Id=@Id and users.IsVerified=1
