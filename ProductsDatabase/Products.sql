CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(30) NOT NULL, 
    [Brand] VARCHAR(20) NOT NULL, 
    [Version] DECIMAL(5, 3) NOT NULL, 
    [Price] MONEY NOT NULL, 
    [RAM] INT NOT NULL, 
    [Year] INT NOT NULL, 
    [Display] INT NOT NULL, 
    [Battery] VARCHAR(30) NOT NULL, 
    [Camera] INT NOT NULL, 
    [Image] VARCHAR(200) NOT NULL
)
