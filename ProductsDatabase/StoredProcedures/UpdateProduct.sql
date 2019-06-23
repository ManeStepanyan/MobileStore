CREATE PROCEDURE [dbo].[UpdateProduct]
	@Id INT,
    @Price MONEY,
	@Image VARCHAR(200)
AS
	update Products 
	set [Price]=IIF(@Price=null,[Price], @Price), 
	[Image]=IIF(@Image=null, [Image], @Image)
	where Id=@Id
GO
