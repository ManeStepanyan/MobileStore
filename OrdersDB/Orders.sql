CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Date] DATE NOT NULL,
	[Address] VARCHAR(50) NOT NULL, 
    [CellPhone] VARBINARY(50) NOT NULL, 
    [Quantity] INT NOT NULL, 
    [Product_ID] INT NOT NULL, 
    [Customer_ID] INT NOT NULL, 
    [TotalAmount] MONEY NOT NULL
)
