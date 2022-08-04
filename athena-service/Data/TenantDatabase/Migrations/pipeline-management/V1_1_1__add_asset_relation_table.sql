CREATE TABLE [${schema}].[AssetRelation] (
    [AssetRelationId]       UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL PRIMARY KEY,
    [AssetId]               UNIQUEIDENTIFIER    NOT NULL,
    [RelationId]            UNIQUEIDENTIFIER    NOT NULL,
    [AssetType]				TINYINT             NOT NULL,
    [RelationType]			TINYINT             NOT NULL,
    [CollectorRelationType]	NVARCHAR(50)        NULL
);