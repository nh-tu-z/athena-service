ALTER Table [${schema}].[AssetActivityLog] ALTER COLUMN [CreatedAt] DATETIME NULL;
ALTER Table [${schema}].[AssetActivityLog] ALTER COLUMN [UserId] UNIQUEIDENTIFIER NULL;