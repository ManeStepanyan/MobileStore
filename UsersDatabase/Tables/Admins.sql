CREATE TABLE [dbo].[Admins] (
    [Id]     INT        IDENTITY (1, 1) NOT NULL,
    [UserId] INT        NOT NULL,
    [Name]   NCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Admins_ToTableRoles] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);


