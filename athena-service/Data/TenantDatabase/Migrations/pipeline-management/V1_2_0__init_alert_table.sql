CREATE TABLE [${schema}].[Alert] (
    [AlertId]     UNIQUEIDENTIFIER  DEFAULT NEWID() NOT NULL,
    [Priority]    TINYINT          NULL,
    [Title]       NVARCHAR (255)   NULL,
    [Description] NVARCHAR (500)   NULL,
    [Confidence]  TINYINT          NULL,
    [DetectedOn]  DATETIME         NOT NULL,
    [Status]      TINYINT          NOT NULL,
    CONSTRAINT [PK_Alert] PRIMARY KEY CLUSTERED ([AlertId] ASC)
);
