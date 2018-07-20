CREATE TABLE [dbo].[Products] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    VARCHAR (30)   NOT NULL,
    [Brand]   VARCHAR (20)   NOT NULL,
    [Version] DECIMAL (5, 3) NOT NULL,
    [Price]   MONEY          NOT NULL,
    [RAM]     INT            NOT NULL,
    [Year]    INT            NOT NULL,
    [Display] DECIMAL (18)   NULL,
    [Battery] VARCHAR (30)   NOT NULL,
    [Camera]  INT            NOT NULL,
    [Image]   VARCHAR (200)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


