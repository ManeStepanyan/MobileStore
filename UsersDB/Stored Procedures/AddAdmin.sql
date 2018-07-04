CREATE PROCEDURE [dbo].[AddAdmin]
	@Name varchar(30),
	@Login varchar(40),
	@Password varchar(25),
	@Roles_ID int
AS
	Insert into Admins([Name], [Login], [Password], Roles_ID)
	Values(@Name, @Login, @Password, @Roles_ID)
GO
