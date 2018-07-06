CREATE PROCEDURE [dbo].[GetAdminByID]
	@Id int
AS
	select * from Admins where Id=@Id
Go
