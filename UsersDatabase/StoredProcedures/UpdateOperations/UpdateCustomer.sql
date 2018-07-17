CREATE PROCEDURE [dbo].[UpdateCustomer]
    @Id int,
	@Name varchar(25),
	@Surname varchar(30),
	@Email varchar(30),

	@Password varchar(25)
AS
begin
declare @UserId int
select @UserId=[UserId] from Customers where Id=@Id
	Update Customers
	set [Name]=IIF(@Name=null,[Name], @Name), 
	[Surname]=IIF(@Surname=null, [Surname],@Surname)
	where Id=@Id
	Update Users
	set [Email]=IIF(@Email=null, [Email],@Email), 
	
	[Password]=IIF(@Password=null, [Password],@Password)
	where Id=@UserId
	
end