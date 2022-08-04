CREATE TABLE [${schema}].[Notification] (
    [NotificationId] UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
	[ObjectId] UNIQUEIDENTIFIER NOT NULL,
	[ObjectType] TINYINT NULL,
    [NotificationType] TINYINT NULL,
	[Description] NVARCHAR(500) NULL,
    [RelateObjectId] UNIQUEIDENTIFIER NULL,
	[RelateObjectType] TINYINT NULL,
    [Status] TINYINT NOT NULL,
    [CreatedAt] DATETIME NOT NULL,
    [UpdatedAt] DATETIME NULL
);