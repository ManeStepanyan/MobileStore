CREATE TABLE [dbo].[Roles]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [Name] NCHAR(20) NOT NULL Unique, 
    [Description] NCHAR(40) NOT NULL
)
