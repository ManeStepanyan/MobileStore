CREATE PROCEDURE [dbo].[GetUserByLogin]
	@Login varchar(40)
AS
	select * from Users
	where [Login]=@Login
