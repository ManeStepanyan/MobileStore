
CREATE PROCEDURE LoginExistsCustomer(
   @Login VARCHAR(40)
)
AS
  IF EXISTS(SELECT * FROM Customers WHERE [Login] = @Login)
  select 1
 else select 2
Go
