CREATE PROCEDURE [dbo].[VerifyUser]
	@ActivationCode varchar(80)
AS
begin
	update Users
	set IsValidated=1
	where ActivationCode=@ActivationCode
end
