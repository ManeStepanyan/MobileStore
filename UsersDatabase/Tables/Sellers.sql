CREATE TABLE [dbo].[Sellers]
(   [Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[User_Id] INT NOT NULL,
    [Name] VARCHAR(30) NOT NULL, 
    [CellPhone] VARCHAR(30) NOT NULL, 
    [Address] VARCHAR(60) NOT NULL Unique,
    [Rating] DECIMAL(5, 2) NULL,
    CONSTRAINT [FK_Sellers_ToTableUsers] FOREIGN KEY ([User_Id]) REFERENCES [Users]([Id])
)
