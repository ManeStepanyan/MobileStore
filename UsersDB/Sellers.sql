CREATE TABLE [dbo].[Sellers]
(
    [Id] INT NOT NULL PRIMARY KEY Identity, 
    [Name] VARCHAR(25) NOT NULL, 
    [CellPhone] BIGINT NOT NULL, 
    [Address] VARCHAR(60) NOT NULL, 
    [Login] VARCHAR(30) NOT NULL, 
    [Password] VARCHAR(25) NOT NULL, 
    [Roles_ID] INT NOT NULL, 
    [Rating] DECIMAL(5, 2) NOT NULL,
    CONSTRAINT [FK_Sellers_ToTableRoles] FOREIGN KEY ([Roles_ID]) REFERENCES [Roles]([Id])
)
