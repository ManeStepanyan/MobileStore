CREATE TABLE [dbo].[Customers] (
    [Id]      INT        IDENTITY (1, 1) NOT NULL,
    [UserId]  INT        NOT NULL,
    [Name]    NCHAR (30) NOT NULL,
    [Surname] NCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Customers_ToTableUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);




