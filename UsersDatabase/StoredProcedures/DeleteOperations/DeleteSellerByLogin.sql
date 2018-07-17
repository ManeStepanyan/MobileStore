CREATE PROCEDURE [dbo].[DeleteSellerByLogin]
	@Login varchar(40)
AS
begin
declare @userId int
select	@userId= Id from Users where [Login]=@Login
delete from Sellers where [UserId]=@userId
delete from Users where [Login]=@Login
end


