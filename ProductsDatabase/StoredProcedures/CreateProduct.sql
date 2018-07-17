CREATE PROCEDURE [dbo].[CreateProduct]
    @Name VARCHAR(30), 
    @Brand VARCHAR(20), 
    @Version DECIMAL(5, 3), 
    @Price MONEY, 
    @RAM INT, 
    @Year INT, 
    @Display INT, 
    @Battery VARCHAR(30), 
    @Camera INT, 
    @Image VARCHAR(200)
AS
	INSERT INTO Products ([Name], [Brand], [Version], [Price], RAM, [Year], Display, Battery, 
							Camera, [Image])
	VALUES (@Name, @Brand, @Version, @Price, @RAM, @Year, @Display, @Battery, @Camera, @Image)
GO