﻿CREATE TABLE [dbo].[Sellers]
(
    [Id] INT NOT NULL PRIMARY KEY Identity, 
    [Name] VARCHAR(25) NOT NULL, 
    [CellPhone] VARCHAR(30) NOT NULL, 
    [Address] VARCHAR(60) NOT NULL Unique, 
    [Login] VARCHAR(30) NOT NULL Unique, 
    [Password] VARCHAR(50) NOT NULL, 
    [Roles_ID] INT NOT NULL, 
    [Rating] DECIMAL(5, 2) NULL,
    CONSTRAINT [FK_Sellers_ToTableRoles] FOREIGN KEY ([Roles_ID]) REFERENCES [Roles]([Id])
)
