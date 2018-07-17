CREATE PROCEDURE [dbo].[UpdateSeller]
    @Id int,
  	@Name varchar(25),
	@Address varchar(60),
	@Email varchar(40),
	@CellPhone varchar(50),
	@Password varchar(25)
AS
begin
declare @UserId int
select @UserId=[UserId] from Sellers where Id=@Id
	Update Sellers
	set [Name]=IIF(@Name =null, [Name],@Name),
	[Address]=IIF(@Address =null, [Address],@Address),
	[CellPhone]=IIF(@CellPhone =null, [CellPhone],@CellPhone)
	where Id=@Id
	Update Users
	set [Email]=IIF(@Email=null, [Email],@Email), 
	[Password]=IIF(@Password=null, [Password],@Password)
	where Id=@UserId
end
