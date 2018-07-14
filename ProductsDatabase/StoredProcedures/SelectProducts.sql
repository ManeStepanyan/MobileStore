CREATE PROCEDURE [dbo].[SelectProducts]
    @Name VARCHAR(30)=null, 
    @Brand VARCHAR(20)=null, 
    @Version DECIMAL(5, 3)=null, 
    @Price MONEY=null,
    @RAM INT =null,
    @Year INT =null,
    @Display INT =null,
    @Battery VARCHAR(30) =null,
    @Camera INT =null,
    @Image VARCHAR(200)=null
AS
	declare @Name1 varchar(30)
	declare @Brand1 varchar(20)
	select @Name1= iif(@Name is null, [Name], @Name)
	select @Brand1= iif(@Brand is null, [Brand], @Brand)
	from Products
	select * from Products
    intersect
	select  *  from Products where [Name]=@Name1 	
	intersect
	select *  from Products where [Brand]=@Brand1 
GO