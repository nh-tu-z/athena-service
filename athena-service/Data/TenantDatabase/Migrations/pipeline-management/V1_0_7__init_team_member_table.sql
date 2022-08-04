CREATE TABLE [${schema}].[TeamMember] (
    [TeamMemberId] UNIQUEIDENTIFIER DEFAULT NEWID()  NOT NULL,
    [PipelineId]   UNIQUEIDENTIFIER                  NOT NULL,
    [UserId]       UNIQUEIDENTIFIER                  NOT NULL,
    [CreatedAt]    DATETIME                          NOT NULL,
    [UpdatedAt]    DATETIME                          NULL,
    CONSTRAINT [PK_TeamMember] PRIMARY KEY CLUSTERED ([TeamMemberId] ASC),
    CONSTRAINT [FK_TeamMember_1] FOREIGN KEY ([PipelineId]) REFERENCES [${schema}].[Pipeline] ([PipelineId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TeamMember_2] FOREIGN KEY ([UserId]) REFERENCES [${schema}].[User] ([UserId])
);
