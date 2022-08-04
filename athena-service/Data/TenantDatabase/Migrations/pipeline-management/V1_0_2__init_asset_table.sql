CREATE TABLE [${schema}].[Asset](
    [AssetId]				  UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    [Name]					  NVARCHAR(255)    NOT NULL,
    [SecurityState]           TINYINT          NOT NULL,
    [Type]					  TINYINT          NOT NULL,
    [TypeDescription]         NVARCHAR(500)    NULL,
    [Source]				  NVARCHAR(255)    NOT NULL,
    [SourceDescription]		  NVARCHAR(500)    NULL,
    [LastActivity]            NVARCHAR(50)     NOT NULL,
    [LastActivityDate]		  DATETIME         NULL,
    [CreatedAt]               DATETIME         NOT NULL,
    [UpdatedAt]               DATETIME		   NOT NULL,
    CONSTRAINT [PK_Asset] PRIMARY KEY CLUSTERED ([AssetId] ASC)
);
