
CREATE TABLE [dbo].[Admins]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(20) NOT NULL, 
    [Login] NCHAR(40) NOT NULL Unique,
    [Password] NCHAR(30) NOT NULL, 
    [Roles_ID] INT NOT NULL, 
    CONSTRAINT [FK_Admins_ToTableRoles] FOREIGN KEY ([Roles_ID]) REFERENCES [Roles]([Id])
)