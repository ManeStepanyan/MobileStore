CREATE PROCEDURE [dbo].[AddCustomer]
	@Name varchar(30),
	@Surname varchar(50),
	@Email varchar(30),
	@Login varchar(40),
	@Password varchar(25),
	@Roles_ID int
AS
	Insert into Customers([Name], Surname, Email, [Login], [Password], [Status],Roles_ID)
	Values(@Name, @Surname, @Email, @Login, @Password, 0, @Roles_ID)
GO