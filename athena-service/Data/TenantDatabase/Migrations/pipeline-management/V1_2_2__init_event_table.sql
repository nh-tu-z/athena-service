CREATE TABLE [${schema}].[Event] (
	[EventId] UNIQUEIDENTIFIER  DEFAULT NEWID() NOT NULL,
	[Title]       NVARCHAR (255)   NULL,
    [Description] NVARCHAR (500)   NULL,
	[Source]	  NVARCHAR (255)   NULL,
	[DetectedOn]  DATETIME         NULL,
	CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([EventId] ASC)
)

CREATE TABLE [${schema}].[EventUser] (
	[EventUserId] UNIQUEIDENTIFIER  DEFAULT NEWID() NOT NULL,
	[EventId] UNIQUEIDENTIFIER NOT NULL,
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_EventUser] PRIMARY KEY CLUSTERED ([EventUserId] ASC),
	CONSTRAINT [FK_EventUser_Event] FOREIGN KEY ([EventId]) REFERENCES [${schema}].[Event] ([EventId]) ON DELETE CASCADE,
    CONSTRAINT [FK_EventUser_User] FOREIGN KEY ([UserId]) REFERENCES [${schema}].[User] ([UserId])
)

CREATE TABLE [${schema}].[EventAsset] (
	[EventAssetId] UNIQUEIDENTIFIER  DEFAULT NEWID() NOT NULL,
	[EventId] UNIQUEIDENTIFIER NOT NULL,
	[AssetId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_EventAsset] PRIMARY KEY CLUSTERED ([EventAssetId] ASC),
	CONSTRAINT [FK_EventAsset_Event] FOREIGN KEY ([EventId]) REFERENCES [${schema}].[Event] ([EventId]) ON DELETE CASCADE,
    CONSTRAINT [FK_EventAsset_Asset] FOREIGN KEY ([AssetId]) REFERENCES [${schema}].[Asset] ([AssetId])
)