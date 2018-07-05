
CREATE TABLE [dbo].[Customers]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [Name] NCHAR(20) NOT NULL, 
    [Surname] NCHAR(30) NOT NULL, 
    [Email] NCHAR(30) NOT NULL, 
    [Login] NCHAR(20) NOT NULL Unique , 
    [Password] NCHAR(50) NOT NULL, 
    [Status] BIT NOT NULL, 
    [Roles_ID] INT NOT NULL, 
    CONSTRAINT [FK_Customers_ToTableRoles] FOREIGN KEY ([Roles_ID]) REFERENCES [Roles]([Id])
)

