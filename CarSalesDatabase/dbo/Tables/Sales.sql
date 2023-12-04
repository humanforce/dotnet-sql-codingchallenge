CREATE TABLE [dbo].[Sales] (
    [ID]    BIGINT           IDENTITY (1, 1) NOT NULL,
    [CarID] UNIQUEIDENTIFIER NOT NULL,
    [Date]  DATE             NOT NULL,
    CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Car_Sales] FOREIGN KEY ([CarID]) REFERENCES [dbo].[Car] ([ID])
);


GO

CREATE NONCLUSTERED INDEX [IX_Sales_Date]
    ON [dbo].[Sales]([Date] ASC);


GO

