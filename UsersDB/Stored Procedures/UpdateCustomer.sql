CREATE PROCEDURE [dbo].[UpdateCustomer]
    @Id int,
	@Name varchar(25),
	@Surname varchar(30),
	@Email varchar(30),
	@Login varchar(30),
	@Password varchar(25)
AS
	Update Customers
	set [Name]= @Name, [Surname]=@Surname, [Email]=@Email, [Login]=@Login, [Password]=@Password
	where Id= @Id
Go
