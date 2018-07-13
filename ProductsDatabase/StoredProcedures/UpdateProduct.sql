CREATE PROCEDURE [dbo].[UpdateProduct]
	@Id INT,
    @Price MONEY
AS
	update Products 
	set [Price]=@Price
	where [Id]=@Id
GO
