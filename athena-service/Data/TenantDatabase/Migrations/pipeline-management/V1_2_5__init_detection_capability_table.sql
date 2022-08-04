CREATE TABLE [${schema}].[DetectionCapability] (
    [DetectionCapabilityId] UNIQUEIDENTIFIER  DEFAULT NEWID() NOT NULL,
	[Name] 	                NVARCHAR (255)   NOT NULL,
	[Description] 	        NVARCHAR (500)   NULL,
    [Severity]              TINYINT          NOT NULL,
    CONSTRAINT [PK_DetectionCapability] PRIMARY KEY CLUSTERED ([DetectionCapabilityId] ASC)
);

ALTER TABLE [${schema}].[Incident] ADD DetectionCapabilityId UNIQUEIDENTIFIER NULL;

CREATE TABLE [${schema}].[DetectionRule] (
    [DetectionRuleId]       UNIQUEIDENTIFIER  DEFAULT NEWID() NOT NULL,
	[DetectionCapabilityId] UNIQUEIDENTIFIER   NOT NULL,
	[AlertRuleId] 	        UNIQUEIDENTIFIER   NOT NULL,
    CONSTRAINT [PK_DetectionRule] PRIMARY KEY CLUSTERED ([DetectionRuleId] ASC),
    CONSTRAINT UC_DetectionRule UNIQUE (DetectionCapabilityId, AlertRuleId)
);