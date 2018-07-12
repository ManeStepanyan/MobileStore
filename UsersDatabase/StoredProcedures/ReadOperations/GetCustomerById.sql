CREATE PROCEDURE [dbo].[GetCustomerById]
@Id int
AS
	SELECT *
	from Customers inner join Users on
	Customers.[User_Id]=Users.Id
	where Customers.Id=@Id
