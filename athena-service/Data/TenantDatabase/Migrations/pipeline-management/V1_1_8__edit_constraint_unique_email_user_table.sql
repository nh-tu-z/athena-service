Alter Table [${schema}].[User] drop constraint Unique_User_Email;
create unique nonclustered index idx_User_Email_notnull on [${schema}].[User](Email) where Email is not null;

CREATE UNIQUE NONCLUSTERED INDEX Idx_AssetRelation_AssetId_RelationId ON [${schema}].[AssetRelation]
(AssetId, RelationId) WITH( IGNORE_DUP_KEY = OFF);