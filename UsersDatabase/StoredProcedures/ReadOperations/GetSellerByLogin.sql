CREATE PROCEDURE [dbo].[GetSellerByLogin]
	@Login varchar(40)
AS
begin
declare @userId int
	SELECT @userId=Id
	from Users where [Login]=@Login
 SELECT *
	from Sellers inner join Users on
	Sellers.[User_Id]=Users.Id
	where Sellers.[User_Id]=@userId
end
