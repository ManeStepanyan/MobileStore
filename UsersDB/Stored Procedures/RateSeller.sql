CREATE PROCEDURE [dbo].[RateSeller]
	@Id int, 
	@Rate decimal(5,2)
AS
Declare @oldRate decimal(5,2)
	SELECT @oldRate= Rating
	from Sellers where Id=@Id
	Update Sellers
	SET Rating= case when @oldRate is  null then @Rate else (@oldRate+@Rate)/2 end
	where Id=@Id
GO
