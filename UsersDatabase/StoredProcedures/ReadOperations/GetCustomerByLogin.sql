CREATE PROCEDURE [dbo].[GetCustomerByLogin]
	@Login varchar(40)
AS
begin
declare @userId int
	SELECT @userId=Id
	from Users where [Login]=@Login
 SELECT *
	from Customers inner join Users on
	Customers.[UserId]=Users.Id
	where Customers.[UserId]=@userId
end