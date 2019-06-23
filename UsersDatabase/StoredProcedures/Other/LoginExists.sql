CREATE PROCEDURE [dbo].[LoginExists](
   @Login VARCHAR(40)
)
AS
  IF EXISTS(SELECT * FROM Users WHERE [Login] = @Login)
  select 1
 else select 2
Go
