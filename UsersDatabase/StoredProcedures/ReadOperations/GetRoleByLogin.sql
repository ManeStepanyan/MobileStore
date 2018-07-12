CREATE PROCEDURE [dbo].[GetRoleByLogin]
	@Login varchar(40)
AS
	select Role_Id from Users
	where [Login]=@Login
