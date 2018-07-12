CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
    [Login] VARCHAR(40) NOT NULL Unique, 
    [Password] VARCHAR(50) NOT NULL, 
    [Email] NCHAR(40) NOT NULL Unique, 
    Role_Id INT NOT NULL,
    CONSTRAINT [FK_Users_ToTableRoles] FOREIGN KEY ([Role_Id]) REFERENCES [Roles]([Id])
)
