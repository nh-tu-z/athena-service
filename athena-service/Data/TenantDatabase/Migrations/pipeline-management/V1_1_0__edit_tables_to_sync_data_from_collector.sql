Alter Table [${schema}].[Asset] Add CollectorAssetId nvarchar(850) not null default newid();
Alter Table [${schema}].[Asset] Add Constraint Unique_Asset_CollectorAssetId unique(CollectorAssetId);
Alter Table [${schema}].[Asset] Add [CreatedBy] UNIQUEIDENTIFIER;
Alter Table [${schema}].[Asset] Add [UpdatedBy] UNIQUEIDENTIFIER;
Alter Table [${schema}].[Asset] Add [Data] nvarchar(4000);
ALTER Table [${schema}].[Asset] ALTER COLUMN [Name] NVARCHAR(255) NULL;
ALTER Table [${schema}].[Asset] ALTER COLUMN [SecurityState] TINYINT NULL;
ALTER Table [${schema}].[Asset] ALTER COLUMN [Type] TINYINT NULL;
ALTER Table [${schema}].[Asset] ALTER COLUMN [Source] NVARCHAR(255) NULL;
ALTER Table [${schema}].[Asset] ALTER COLUMN [CreatedAt] DATETIME NULL;
ALTER Table [${schema}].[Asset] ALTER COLUMN [UpdatedAt] DATETIME NULL;
Alter Table [${schema}].[Asset] Add [IsDeleted] bit;

Alter Table [${schema}].[User] Add CollectorAssetId nvarchar(850) not null default newid();
Alter Table [${schema}].[User] Add Constraint Unique_User_CollectorAssetId unique(CollectorAssetId);
ALTER Table [${schema}].[User] ALTER COLUMN [FirstName] NVARCHAR(50) NULL;
ALTER Table [${schema}].[User] ALTER COLUMN [LastName] NVARCHAR(50) NULL;
ALTER Table [${schema}].[User] ALTER COLUMN [Email] NVARCHAR(255) NULL;
ALTER Table [${schema}].[User] ALTER COLUMN [CreatedAt] DATETIME NULL;