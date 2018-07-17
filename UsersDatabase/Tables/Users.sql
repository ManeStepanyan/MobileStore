CREATE TABLE [dbo].[Users] (
    [Id]             INT          IDENTITY (1, 1) NOT NULL,
    [Login]          VARCHAR (40) NOT NULL,
    [Password]       VARCHAR (50) NOT NULL,
    [Email]          NCHAR (40)   NOT NULL,
    [RoleId]         INT          NOT NULL,
    [IsVerified]     BIT          NULL,
    [IsActive]       BIT          NULL,
    [ActivationCode] VARCHAR (80) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Users_ToTableRoles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]),
    UNIQUE NONCLUSTERED ([Email] ASC),
    UNIQUE NONCLUSTERED ([Login] ASC)
);








