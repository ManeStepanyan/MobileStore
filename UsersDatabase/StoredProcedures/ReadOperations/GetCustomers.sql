CREATE PROCEDURE [dbo].[GetCustomers]
AS
	SELECT *
	from Customers join Users
	on Customers.[User_Id]=Users.Id
