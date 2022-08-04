CREATE TABLE [${schema}].[Enum](
	[EnumTypeId] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Enum] PRIMARY KEY CLUSTERED 
(
	[EnumTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [${schema}].[EnumDetail](
	[EnumId] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[EnumTypeId] [int] NULL,
	[Value] [int] NOT NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_EnumDetail] PRIMARY KEY CLUSTERED 
(
	[EnumId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [${schema}].[EnumDetail]  WITH CHECK ADD  CONSTRAINT [FK_EnumDetail_Enum] FOREIGN KEY([EnumTypeId])
REFERENCES [${schema}].[Enum] ([EnumTypeId])

ALTER TABLE [${schema}].[EnumDetail] CHECK CONSTRAINT [FK_EnumDetail_Enum]


SET IDENTITY_INSERT [${schema}].[Enum] ON 
INSERT [${schema}].[Enum] ([EnumTypeId], [Description]) VALUES (1, N'AssetType')
SET IDENTITY_INSERT [${schema}].[Enum] OFF

SET IDENTITY_INSERT [${schema}].[EnumDetail] ON 
INSERT [${schema}].[EnumDetail] ([EnumId], [EnumTypeId], [Value], [Description]) VALUES (1, 1, 1, N'Experiment')
INSERT [${schema}].[EnumDetail] ([EnumId], [EnumTypeId], [Value], [Description]) VALUES (2, 1, 2, N'Dataset')
INSERT [${schema}].[EnumDetail] ([EnumId], [EnumTypeId], [Value], [Description]) VALUES (3, 1, 3, N'Model')
INSERT [${schema}].[EnumDetail] ([EnumId], [EnumTypeId], [Value], [Description]) VALUES (4, 1, 4, N'Deployment')
SET IDENTITY_INSERT [${schema}].[EnumDetail] OFF



