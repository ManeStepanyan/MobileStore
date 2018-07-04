CREATE PROCEDURE [dbo].[DeleteAdmin]
	@Id int
AS
	Delete from Admins
	where Id=@Id
GO
