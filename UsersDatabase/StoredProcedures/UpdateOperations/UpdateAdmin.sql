CREATE PROCEDURE [dbo].[UpdateAdmin]
    @Id int,
	@Name varchar(20),
	@Email varchar(40),

	@Password varchar(30)
AS
begin
declare @UserId int
select UserId= [UserId] from Admins where Id=@Id
	Update Admins
	set [Name]=IIF(@Name=null, [Name],@Name)
	where Id=@Id
	Update Users
	set [Email]= IIF(@Email=null,[Email],@Email),
	[Password]=IIF(@Password=null,[Password],@Password)
	where Id=@UserId
	end
