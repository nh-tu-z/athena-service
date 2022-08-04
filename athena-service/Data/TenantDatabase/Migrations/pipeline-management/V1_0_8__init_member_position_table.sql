CREATE TABLE [${schema}].[MemberPosition] (
    [PositionId]        UNIQUEIDENTIFIER DEFAULT NEWID()    NOT NULL,
    [TeamMemberId]      UNIQUEIDENTIFIER                    NOT NULL,
    [Position]          INT                                 NOT NULL,
    [CreatedAt]         DATETIME                            NOT NULL,
    [UpdatedAt]         DATETIME                            NULL,
    CONSTRAINT [PK_MemberPosition] PRIMARY KEY CLUSTERED ([PositionId] ASC),
    CONSTRAINT [FK_MemberPosition_1] FOREIGN KEY ([TeamMemberId]) REFERENCES [${schema}].[TeamMember] ([TeamMemberId]) ON DELETE CASCADE
);
