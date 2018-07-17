CREATE PROCEDURE [dbo].[SelectProducts]
    @Name VARCHAR(30)=null, 
    @Brand VARCHAR(20)=null, 
    @Version DECIMAL(5, 3)=null, 
    @PriceFrom MONEY=null,
	@PriceTo MONEY=null,
    @RAM INT =null,
    @Year INT =null,
    @Display INT =null,
    @Battery VARCHAR(30) =null,
    @Camera INT =null,
    @Image VARCHAR(200)=null
AS
	declare @Name1 varchar(30)
	declare @Brand1 varchar(20)
	declare @Version1 decimal(5, 3)
	declare @Price1 money
	declare @Price2 money
	declare @RAM1 int
	declare @Year1 int 
	declare @Display1 int 
	declare @Battery1 varchar(30)
	declare @Camera1 int
	declare @Image1 varchar(200)
	select @Name1= iif(@Name is null, [Name], @Name)
	from Products
	select @Brand1= iif(@Brand is null, [Brand], @Brand)
	from Products
	select @Version1= iif(@Version is null, [Version], @Version)
	from Products
	select @Price1= iif(@PriceFrom is null, [Price], @PriceFrom)
	from Products
	select @Price2= iif(@PriceTo is null, [Price], @PriceTo)
	from Products
	select @RAM1= iif(@RAM is null, [RAM], @RAM)
	from Products
	select @Year1= iif(@Year is null, [Year], @Year)
	from Products
	select @Display1= iif(@Display is null, [Display], @Display)
	from Products
	select @Battery1= iif(@Battery is null, [Battery], @Battery)
	from Products
	select @Camera1= iif(@Camera is null, [Camera], @Camera)
	from Products
	select @Image1= iif(@Image1 is null, [Image], @Image)
	from Products
	select * from Products
    intersect
	select  *  from Products where [Name]=@Name1 	
	intersect
	select *  from Products where [Brand]=@Brand1 
	intersect
	select *  from Products where [Version]=@Version1 
	intersect
	select *  from Products where [Price] between @Price1  and @Price2
	intersect
	select *  from Products where [RAM]=@RAM1 
	intersect
	select *  from Products where [Year]=@Year1 
	intersect
	select *  from Products where [Display]=@Display1 
	intersect
	select *  from Products where [Battery]=@Battery1 
	intersect
	select *  from Products where [Camera]=@Camera1 
	intersect
	select *  from Products where [Image]=@Image1
	order by Price
GO