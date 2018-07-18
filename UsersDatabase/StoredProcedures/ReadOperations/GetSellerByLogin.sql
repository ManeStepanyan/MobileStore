CREATE PROCEDURE [dbo].[GetSellerByLogin]
	@Login varchar(40)
AS
begin
declare @userId int
	SELECT @userId=Id
	from Users where [Login]=@Login and IsVerified=1
 SELECT *
	from Sellers inner join Users on
	Sellers.[UserId]=Users.Id
	where Sellers.[UserId]=@userId
end
