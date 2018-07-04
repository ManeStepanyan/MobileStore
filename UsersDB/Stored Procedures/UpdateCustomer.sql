CREATE PROCEDURE [dbo].[UpdateCustomer]
    @Id int,
	@Name varchar(30),
	@Surname varchar(50),
	@Email varchar(30),
	@Login varchar(40),
	@Password varchar(25),
	@Roles_ID int
AS
	Update Customers
	set [Name]=@Name, Surname=@Surname, Email=@Email, [Login]=@Login, [Password]=@Password, Roles_ID=@Roles_ID
	where Id=@Id
GO
