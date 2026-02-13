USE [InterviewCoachDB]
GO

/****** Object:  Table [ic].[Topics]    Script Date: 2/13/2026 4:55:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [ic].[Topics](
	[Id] [uniqueidentifier] NOT NULL,
	[TechId]  [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Slug] [nvarchar](200) NOT NULL,
	[ParentTopicId] [uniqueidentifier] NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[LastUtcModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Topics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [ic].[Topics] ADD  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [ic].[Topics] ADD  DEFAULT ((0)) FOR [DisplayOrder]
GO

ALTER TABLE [ic].[Topics] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [ic].[Topics] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedUtcDate]
GO

ALTER TABLE [ic].[Topics] ADD  DEFAULT (sysutcdatetime()) FOR [LastUtcModified]
GO

ALTER TABLE [ic].[Topics]  WITH CHECK ADD  CONSTRAINT [FK_Topics_Parent] FOREIGN KEY([ParentTopicId])
REFERENCES [ic].[Topics] ([Id])
GO

ALTER TABLE [ic].[Topics] CHECK CONSTRAINT [FK_Topics_Parent]
GO
ALTER TABLE [ic].[Topics] ADD  CONSTRAINT [FK_Topics_Technology] FOREIGN KEY([TechId])
REFERENCES [ic].[Technology] ([Id])


