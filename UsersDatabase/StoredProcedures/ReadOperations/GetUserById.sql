CREATE PROCEDURE [dbo].[GetUserById]
@Id int
AS
	select * from Users
	where Id=@Id

