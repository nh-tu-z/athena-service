Alter Table [${schema}].[User] Add Constraint Unique_User_Email unique(Email);

Alter Table [${schema}].[User] drop constraint Unique_User_CollectorAssetId;
ALTER Table [${schema}].[User] ALTER COLUMN [CollectorAssetId] nvarchar(850) NULL;

DECLARE @tableName VARCHAR(MAX) = '[${schema}].[User]';
DECLARE @columnName VARCHAR(MAX) = 'CollectorAssetId';
DECLARE @ConstraintName nvarchar(200);
SELECT @ConstraintName = Name 
FROM SYS.DEFAULT_CONSTRAINTS
WHERE PARENT_OBJECT_ID = OBJECT_ID(@tableName) 
AND PARENT_COLUMN_ID = (
    SELECT column_id FROM sys.columns
    WHERE NAME = @columnName AND object_id = OBJECT_ID(@tableName));
IF @ConstraintName IS NOT NULL
    EXEC('ALTER TABLE '+@tableName+' DROP CONSTRAINT ' + @ConstraintName);

create unique nonclustered index idx_User_CollectorAssetId_notnull on [${schema}].[User](CollectorAssetId) where CollectorAssetId is not null;