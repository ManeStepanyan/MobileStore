CREATE PROCEDURE [dbo].[GetUser]
	@Id int
AS
begin
	SELECT * from Users where Id=@Id
end
