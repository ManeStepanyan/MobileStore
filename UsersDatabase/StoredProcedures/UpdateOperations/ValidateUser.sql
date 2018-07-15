CREATE PROCEDURE [dbo].[ValidateUser]
	@UserId int
AS
begin
	UPDATE Users
	set IsValidated=1
	where Id=@UserId
end
