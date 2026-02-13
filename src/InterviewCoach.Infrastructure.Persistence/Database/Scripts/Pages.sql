USE [InterviewCoachDB]
GO

/****** Object:  Table [ic].[Pages]    Script Date: 2/13/2026 4:52:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [ic].[Pages](
	[Id] [uniqueidentifier] NOT NULL,
	[TopicId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Slug] [nvarchar](200) NOT NULL,
	[Summary] [nvarchar](500) NULL,
	[IsPublished] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[LastUtcModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [ic].[Pages] ADD  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [ic].[Pages] ADD  DEFAULT ((0)) FOR [IsPublished]
GO

ALTER TABLE [ic].[Pages] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedUtcDate]
GO

ALTER TABLE [ic].[Pages] ADD  DEFAULT (sysutcdatetime()) FOR [LastUtcModified]
GO

ALTER TABLE [ic].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Topics] FOREIGN KEY([TopicId])
REFERENCES [ic].[Topics] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [ic].[Pages] CHECK CONSTRAINT [FK_Pages_Topics]
GO


