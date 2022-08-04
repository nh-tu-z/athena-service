CREATE TABLE [${schema}].[AssetActivityLog] (
    [AssetActivityLogId]    UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL PRIMARY KEY,
    [Activity]	            NVARCHAR(500)       NOT NULL,
    [CreatedAt]             DATETIME            NOT NULL,
    [UserId]                UNIQUEIDENTIFIER    NOT NULL,
    [AssetId]               UNIQUEIDENTIFIER    NOT NULL
);