CREATE TABLE [${schema}].[Integration] (
    [IntegrationId] UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    [IntegrationName] NVARCHAR (255) NOT NULL,
    [Description] NVARCHAR (500) NOT NULL,
    [Environment] TINYINT NOT NULL,
    [TokenId] UNIQUEIDENTIFIER NOT NULL UNIQUE,
	[State] TINYINT NULL,
    [CreatedBy] BIGINT NOT NULL,
    [CreatedAt] DATETIME NOT NULL,
    [StateUpdatedAt] DATETIME NULL
);
