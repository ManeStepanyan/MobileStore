
CREATE TABLE [dbo].[Admin]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(20) NOT NULL, 
    [Login] NCHAR(20) NOT NULL, 
    [Password] NCHAR(30) NOT NULL, 
    [Roles_ID] INT NOT NULL, 
    CONSTRAINT [FK_Admin_ToTableRoles] FOREIGN KEY ([Roles_ID]) REFERENCES [Roles]([Id])
)