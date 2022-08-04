CREATE TABLE [${schema}].[AlertRule] (
    [AlertRuleId] UNIQUEIDENTIFIER  DEFAULT NEWID() NOT NULL,
	[RuleName] 	  NVARCHAR (255)   NULL,
    [Priority]    TINYINT          NULL,
    [CreatedOn]   DATETIME         NOT NULL,
    [Status]      TINYINT          NOT NULL,
    CONSTRAINT [PK_AlertRule] PRIMARY KEY CLUSTERED ([AlertRuleId] ASC)
);


ALTER TABLE [${schema}].[Alert]
  ADD [AlertRuleId] UNIQUEIDENTIFIER