CREATE TABLE [${schema}].[User] (
    [UserId]        UNIQUEIDENTIFIER DEFAULT NEWID()    NOT NULL,
    [FirstName]     NVARCHAR(50)                        NOT NULL,
    [LastName]      NVARCHAR(50)                        NOT NULL,
    [Email]         NVARCHAR(255)                       NOT NULL,
    [CreatedAt]     DATETIME                            NOT NULL,
    [UpdatedAt]     DATETIME                            NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC)
);
