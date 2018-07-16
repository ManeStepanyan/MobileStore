CREATE TABLE [dbo].[Sellers] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [UserId]    INT            NOT NULL,
    [Name]      VARCHAR (30)   NOT NULL,
    [CellPhone] VARCHAR (30)   NOT NULL,
    [Address]   VARCHAR (60)   NOT NULL,
    [Rating]    DECIMAL (5, 2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Sellers_ToTableUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
    UNIQUE NONCLUSTERED ([Address] ASC)
);


