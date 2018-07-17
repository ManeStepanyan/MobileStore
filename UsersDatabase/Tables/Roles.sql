CREATE TABLE [dbo].[Roles] (
    [Id]          INT        IDENTITY (1, 1) NOT NULL,
    [Name]        NCHAR (20) NOT NULL,
    [Description] NCHAR (40) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);


