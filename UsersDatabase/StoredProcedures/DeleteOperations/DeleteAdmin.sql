CREATE PROCEDURE [dbo].[DeleteAdmin]
	@Id int
AS
begin
declare @userId int
select @userId= [User_Id] from Admins where Id=@Id
delete from Admins where Id=@Id
delete from Users where Id=@userId
end
