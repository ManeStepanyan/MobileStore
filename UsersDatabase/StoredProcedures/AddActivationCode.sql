CREATE PROCEDURE [dbo].[AddActivationCode]
	@Id int, 
	@ActivationCode VARCHAR(80)
AS
begin
	update Users
	set ActivationCode=@ActivationCode
	where Id=@Id
end
