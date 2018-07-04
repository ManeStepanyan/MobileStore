CREATE PROCEDURE [dbo].[AddSeller]
	@Name varchar(25),
	@Address varchar(60),
	@CellPhone bigint,
	@Login varchar(30),
	@Password varchar(25),
	@Roles_ID int
AS
	Insert into Sellers([Name], CellPhone, [Address], [Login], [Password],Roles_ID,Rating)
	Values(@Name,@CellPhone,@Address,@Login,@Password,@Roles_ID,NULL)
Go
