ALTER TABLE [${schema}].[AssetActivityLog]
ADD EventType TINYINT, 
    [Description] NVARCHAR(255) NULL, 
    [Source] NVARCHAR(255) NULL

GO

CREATE TABLE [${schema}].[AssetInvolvedActivityLog]
(
    AssetInvolvedActivityLogId UNIQUEIDENTIFIER NOT NULL,
    [Activity] NVARCHAR(500) NOT NULL,
    [Description] NVARCHAR(255) NULL,
    [CreatedAt] DATETIME NULL,
	[UserId] UNIQUEIDENTIFIER NULL,
	[AssetId] UNIQUEIDENTIFIER NULL,
	[AssetActivityLogId] UNIQUEIDENTIFIER NULL,
	CONSTRAINT [PK_AssetInvolvedActivityLog] PRIMARY KEY CLUSTERED (AssetInvolvedActivityLogId ASC)
)

GO

ALTER TABLE [${schema}].[AlertRule]
ADD EventType TINYINT
    
GO

CREATE TABLE [${schema}].[AlertRuleExecution]
(
    AlertRuleExecutionId UNIQUEIDENTIFIER NOT NULL,
    AlertRuleId UNIQUEIDENTIFIER NOT NULL,
    RunAt DATETIME NOT NULL,
    CONSTRAINT [PK_AlertRuleExecution] PRIMARY KEY CLUSTERED (AlertRuleExecutionId ASC),
	CONSTRAINT [FK_AlertRuleExecution_AlertRule] FOREIGN KEY (AlertRuleId) REFERENCES [${schema}].[AlertRule] (AlertRuleId)
)

GO

CREATE TABLE [${schema}].[AlertRuleExecutionDetail]
(
    AlertRuleExecutionDetailId UNIQUEIDENTIFIER NOT NULL,
    AlertRuleExecutionId UNIQUEIDENTIFIER NOT NULL,
    AssetActivityLogId UNIQUEIDENTIFIER,
    CONSTRAINT [PK_AlertRuleExecutionDetail] PRIMARY KEY CLUSTERED (AlertRuleExecutionDetailId ASC),
	CONSTRAINT [FK_AlertRuleExecutionDetail_AlertRuleExecution] FOREIGN KEY (AlertRuleExecutionId) REFERENCES [${schema}].[AlertRuleExecution] (AlertRuleExecutionId) ON DELETE CASCADE
)

GO

ALTER TABLE [${schema}].[Alert]
DROP COLUMN AlertRuleId

GO
ALTER TABLE [${schema}].[Alert]
ADD AlertRuleExecutionDetailId UNIQUEIDENTIFIER NULL