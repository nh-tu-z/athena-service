CREATE TABLE [${schema}].[Incident] (
    [IncidentId] UNIQUEIDENTIFIER  DEFAULT NEWID() NOT NULL,
	[Severity] 	  TINYINT   NULL,
	[Title] 	  NVARCHAR (255)   NOT NULL,
	[Description] 	  NVARCHAR (500)   NULL,
    [Confidence]    TINYINT          NULL,
    [ReportedOn]   DATETIME         NOT NULL,
    [Status]      TINYINT          NOT NULL,
    CONSTRAINT [PK_Incident] PRIMARY KEY CLUSTERED ([IncidentId] ASC)
);
