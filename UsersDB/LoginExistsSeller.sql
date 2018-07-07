
CREATE PROCEDURE LoginExistsSeller(
   @Login VARCHAR(40)
)
AS
  IF EXISTS(SELECT * FROM Sellers WHERE [Login] = @Login)
  select 1
 else select 2
Go