CREATE PROCEDURE [dbo].[SelectProducts]
	@Id INT NOT NULL, 
    @Name VARCHAR(30), 
    @Brand VARCHAR(20), 
    @Version DECIMAL(5, 3), 
    @Price MONEY, 
    @RAM INT, 
    @Year INT, 
    @Display INT, 
    @Battery VARCHAR(30), 
    @Camera INT, 
    @Image VARCHAR(200), 
    @Sellers_ID INT 
AS
	declare @sqlName nvarchar(100)
	declare @sqlBrand nvarchar(100)

	if(@Name is not null)
		select @sqlName='select *  from Products where [Name]=@Name'
	else
		select @sqlName='select *  from Products'
	if(@Brand is not null)
		select @sqlBrand='select *  from Products where [Brand]=@Brand'
	else
		select @sqlBrand='select *  from Products'
	select @sqlName
	intersect
	select @sqlBrand
GO
