CREATE PROCEDURE [dbo].[GetCustomerById]
@Id int
AS
	SELECT *
	from Customers inner join Users on
	Customers.[UserId]=Users.Id
	where Customers.Id=@Id
