CREATE PROCEDURE [dbo].[GetSellerByUserId]
	@userid int
AS
begin
	SELECT * from Sellers where UserId=@userid
end
