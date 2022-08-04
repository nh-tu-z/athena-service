CREATE TABLE [${schema}].[AssetPipeline](
    [AssetPipelineId]		  UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
	[AssetId]			      UNIQUEIDENTIFIER NOT NULL,
    [PipelineId]			  UNIQUEIDENTIFIER NOT NULL,
    [CreatedAt]               DATETIME         NOT NULL,
    [UpdatedAt]               DATETIME		   NOT NULL,
    CONSTRAINT [PK_AssetPipeline] PRIMARY KEY CLUSTERED ([AssetPipelineId] ASC),
	CONSTRAINT [FK_AssetPipeline_1] FOREIGN KEY ([PipelineId]) REFERENCES [${schema}].[Pipeline] ([PipelineId]) ON DELETE CASCADE,
    CONSTRAINT [FK_AssetPipeline_2] FOREIGN KEY ([AssetId]) REFERENCES [${schema}].[Asset] ([AssetId])
);
