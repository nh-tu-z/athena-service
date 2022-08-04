CREATE TABLE [${schema}].[Tag] (
    [TagId]       UNIQUEIDENTIFIER DEFAULT NEWID()  NOT NULL,
    [TagCategory] INT                               NOT NULL,
    [TagName]     NVARCHAR (255)                    NOT NULL,
    [CreatedAt]   DATETIME                          NOT NULL,
    [UpdatedAt]   DATETIME                          NULL,
    CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED ([TagId] ASC)
);