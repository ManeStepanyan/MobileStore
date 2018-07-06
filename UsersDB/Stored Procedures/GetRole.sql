CREATE PROCEDURE [dbo].[GetRole]
@Id int
AS
select * from Roles
where Id=@Id
Go
