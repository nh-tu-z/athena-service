CREATE TABLE [${schema}].[TagPipeline] (
    [TagPipelineId] UNIQUEIDENTIFIER DEFAULT NEWID()    NOT NULL,
    [PipelineId]    UNIQUEIDENTIFIER                    NOT NULL,
    [TagId]         UNIQUEIDENTIFIER                    NOT NULL,
    [CreatedAt]     DATETIME                            NOT NULL,
    [UpdatedAt]     DATETIME                            NULL,
    CONSTRAINT [PK_TagPipeline] PRIMARY KEY CLUSTERED ([TagPipelineId] ASC),
    CONSTRAINT [FK_TagPipeline_1] FOREIGN KEY ([PipelineId]) REFERENCES [${schema}].[Pipeline] ([PipelineId])  ON DELETE CASCADE,
    CONSTRAINT [FK_TagPipeline_2] FOREIGN KEY ([TagId]) REFERENCES [${schema}].[Tag] ([TagId])  ON DELETE CASCADE
);