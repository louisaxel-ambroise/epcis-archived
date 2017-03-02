CREATE SCHEMA [epcis] AUTHORIZATION [dbo];
GO

CREATE TABLE [epcis].[Event](
	[Id] [bigint] IDENTITY(0,1) NOT NULL,
	[EventType] [smallint] NOT NULL,
	[EventTime] [datetime2](7) NOT NULL,
	[EventTimezoneOffset] [smallint] NOT NULL,
	[CaptureTime] [datetime2](7) NOT NULL,
	[Action] [smallint] NULL,
	[BusinessStep] [nvarchar](128) NULL,
	[Disposition] [nvarchar](128) NULL,
	[EventId] [nvarchar](128) NULL,
	[Ilmd] [xml] NULL,
	[TransformationId] [nvarchar](128) NULL,
	[CustomFields] [xml] NULL,
	CONSTRAINT [PK_EPCIS] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [epcis].[ErrorDeclarationEventId](
	[EventId] [bigint] NOT NULL,
	[ReferencedId] [nvarchar](128) NOT NULL,
	CONSTRAINT [PK_ErrorDeclarationEventId] PRIMARY KEY CLUSTERED ([EventId] ASC,[ReferencedId] ASC)
);

CREATE TABLE [epcis].[SourceDestination](
	[EventId] [bigint] NOT NULL,
	[Type] [nvarchar](128) NOT NULL,
	[SourceDestId] [nvarchar](128) NOT NULL,
	[Direction] [nvarchar](1) NOT NULL,
	CONSTRAINT [PK_SOURCEDESTINATION] PRIMARY KEY CLUSTERED ([EventId] ASC,	[Type] ASC)
);

CREATE TABLE [epcis].[ReadPoint](
	[EventId] [bigint] NOT NULL,
	[ReadPointId] [nvarchar](128) NOT NULL,
	[CustomFields] [xml] NULL,
	CONSTRAINT [EVENT_READPOINT_ID] PRIMARY KEY CLUSTERED ([EventId] ASC)
);

CREATE TABLE [epcis].[ErrorDeclaration](
	[EventId] [bigint] NOT NULL,
	[DeclarationTime] [datetime] NOT NULL,
	[Reason] [nvarchar](128) NOT NULL,
	[CustomFields] [xml] NULL,
	CONSTRAINT [PK_ErrorDeclaration] PRIMARY KEY CLUSTERED ([EventId] ASC)
);

CREATE TABLE [epcis].[Epc](
	[EventId] [bigint] NOT NULL,
	[Epc] [nvarchar](128) NOT NULL,
	[Type] [smallint] NOT NULL,
	[IsQuantity] [bit] NOT NULL,
	[Quantity] [float] NULL,
	[UnitOfMeasure] [nvarchar](3) NULL,
	CONSTRAINT [PK_EVENT_EPC] PRIMARY KEY CLUSTERED ([EventId] ASC, [Epc] ASC)
);

CREATE TABLE [epcis].[BusinessTransaction](
	[EventId] [bigint] NOT NULL,
	[TransactionType] [nvarchar](128) NOT NULL,
	[TransactionId] [nvarchar](128) NOT NULL,
	[CustomFields] [xml] NULL,
	CONSTRAINT [PK_EVENT_BUSINESS_TRANSACTION] PRIMARY KEY CLUSTERED ([EventId] ASC,[TransactionType] ASC)
);

CREATE TABLE [epcis].[BusinessLocation](
	[EventId] [bigint] NOT NULL,
	[BusinessLocationId] [nvarchar](128) NOT NULL,
	[CustomFields] [xml] NULL,
	CONSTRAINT [EVENT_BIZLOC_ID] PRIMARY KEY CLUSTERED ([EventId] ASC)
);

ALTER TABLE [epcis].[Event] ADD CONSTRAINT [DF_EpcisEvent_CaptureTime]  DEFAULT (getutcdate()) FOR [CaptureTime];
ALTER TABLE [epcis].[BusinessLocation]  WITH CHECK ADD CONSTRAINT [FK_BIZLOC_EVENT] FOREIGN KEY([EventId]) REFERENCES [epcis].[Event] ([Id]);
ALTER TABLE [epcis].[BusinessTransaction]  WITH CHECK ADD CONSTRAINT [FK_BIZTRANS_EVENT] FOREIGN KEY([EventId]) REFERENCES [epcis].[Event] ([Id]);
ALTER TABLE [epcis].[Epc]  WITH CHECK ADD CONSTRAINT [FK_EPC_EVENT] FOREIGN KEY([EventId]) REFERENCES [epcis].[Event] ([Id]);
ALTER TABLE [epcis].[ErrorDeclaration]  WITH CHECK ADD  CONSTRAINT [FK_ERRORDECLARATION_EVENT] FOREIGN KEY([EventId]) REFERENCES [epcis].[Event] ([Id]);
ALTER TABLE [epcis].[ReadPoint]  WITH CHECK ADD  CONSTRAINT [FK_READPOINT_EVENT] FOREIGN KEY([EventId]) REFERENCES [epcis].[Event] ([Id]);
ALTER TABLE [epcis].[SourceDestination]  WITH CHECK ADD  CONSTRAINT [FK_SOURCEDESTINATION_EVENT] FOREIGN KEY([EventId]) REFERENCES [epcis].[Event] ([Id]);
GO
