CREATE TABLE [dbo].[Car] (
    [ID]     UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]   NVARCHAR (128)   NOT NULL,
    [Colour] NVARCHAR (64)    NOT NULL,
    [Price]  DECIMAL (14, 2)  NOT NULL,
    CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
