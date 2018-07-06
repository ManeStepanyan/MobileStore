CREATE PROCEDURE [dbo].[ChangeStatus]
    @Id int,
	@Status bit

AS
	Update Customers
	Set Status=@Status
	where Id=@Id
Go
