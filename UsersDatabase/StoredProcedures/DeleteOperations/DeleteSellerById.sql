CREATE PROCEDURE [dbo].[DeleteSellerById]
	@Id int
AS
begin
declare @userId int
select @userId= [User_Id] from Sellers where Id=@Id
delete from Sellers where Id=@Id
delete from Users where Id=@userId
end
