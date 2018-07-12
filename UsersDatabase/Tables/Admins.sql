CREATE TABLE [dbo].[Admins]
(	[Id] INT NOT NULL Primary key Identity,
	[User_Id] INT NOT NULL, 
    [Name] NCHAR(30) NOT NULL,
    CONSTRAINT [FK_Admins_ToTableRoles] FOREIGN KEY ([User_Id]) REFERENCES [Users]([Id])
)
