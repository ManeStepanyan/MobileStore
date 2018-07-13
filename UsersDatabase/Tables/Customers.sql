CREATE TABLE [dbo].[Customers]
(	
	[Id] INT NOT NULL Primary key identity,
	[User_Id] INT NOT NULL,
    [Name] NCHAR(30) NOT NULL, 
    [Surname] NCHAR(30) NOT NULL, 
    [Status] BIT NOT NULL, 
    CONSTRAINT [FK_Customers_ToTableUsers] FOREIGN KEY ([User_Id]) REFERENCES [Users]([Id])
)
