CREATE PROCEDURE [dbo].[DeleteAdmin]
	@Id int
AS
begin
declare @userId int
select @userId= [UserId] from Admins where Id=@Id
delete from Admins where Id=@Id
delete from Users where Id=@userId
end
