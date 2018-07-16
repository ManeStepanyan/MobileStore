CREATE PROCEDURE [dbo].[GetCustomers]
AS
	SELECT *
	from Customers join Users
	on Customers.[UserId]=Users.Id
