CREATE PROCEDURE [dbo].[GetCustomerByName]
	@Login varchar(40)
AS
select * from Customers 
where [Login]=@Login
	Go
