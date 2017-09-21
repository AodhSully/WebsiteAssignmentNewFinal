CREATE TABLE [dbo].[Record] (
    [TrackID]   NVARCHAR (50)  NOT NULL,
    [Artist]    NVARCHAR (100) NULL,
    [Track]     NVARCHAR (100) NOT NULL,
    [Price]     DECIMAL (18, 2)   NOT NULL,
    [Artwork]   VARCHAR (50)   NULL,
    [Audio]     VARCHAR (50)   NULL,
    [ProductID] INT            NULL,
    FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID])
);

