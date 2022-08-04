CREATE TABLE [${schema}].[Pipeline] (
    [PipelineId]              UNIQUEIDENTIFIER DEFAULT NEWID()  NOT NULL,
    [PipelineName]            NVARCHAR (255)                    NOT NULL,
    [Description]             NVARCHAR (500)                    NOT NULL,
    [SecurityScore]           TINYINT                           NOT NULL,
    [Criticality]             TINYINT                           NOT NULL,
    [State]                   TINYINT                           NOT NULL,
    [LastActivity]            NVARCHAR (50)                     NOT NULL,
    [LastActivityTimeStamp]   DATETIME                          NOT NULL,
    [CreatedAt]               DATETIME                          NOT NULL,
    [UpdatedAt]               DATETIME                          NULL,
    CONSTRAINT [PK_Pipeline] PRIMARY KEY CLUSTERED ([PipelineId] ASC),
    CONSTRAINT [security_score_in_range] CHECK (SecurityScore BETWEEN 0 AND 100),
    CONSTRAINT [criticality_in_range] CHECK (Criticality BETWEEN 1 AND 3),
    CONSTRAINT [state_in_range] CHECK (State BETWEEN 1 AND 5)
);
