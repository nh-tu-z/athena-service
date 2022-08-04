ALTER TABLE [${schema}].[Pipeline] ALTER COLUMN [Description] NVARCHAR (500) NULL;
ALTER TABLE [${schema}].[Pipeline] ALTER COLUMN [Criticality] TINYINT NULL;
ALTER TABLE [${schema}].[Pipeline] DROP CONSTRAINT [criticality_in_range];