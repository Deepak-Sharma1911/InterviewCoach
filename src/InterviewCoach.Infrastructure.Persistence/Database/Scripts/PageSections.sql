USE [InterviewCoachDB]
GO

/****** Object:  Table [ic].[PageSections]    Script Date: 2/13/2026 4:53:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [ic].[PageSections](
	[Id] [uniqueidentifier] NOT NULL,
	[PageId] [uniqueidentifier] NOT NULL,
	[SectionType] [int] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[LastUtcModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PageSections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [ic].[PageSections] ADD  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [ic].[PageSections] ADD  DEFAULT ((0)) FOR [DisplayOrder]
GO

ALTER TABLE [ic].[PageSections] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedUtcDate]
GO

ALTER TABLE [ic].[PageSections] ADD  DEFAULT (sysutcdatetime()) FOR [LastUtcModified]
GO

ALTER TABLE [ic].[PageSections]  WITH CHECK ADD  CONSTRAINT [FK_PageSections_Pages] FOREIGN KEY([PageId])
REFERENCES [ic].[Pages] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [ic].[PageSections] CHECK CONSTRAINT [FK_PageSections_Pages]
GO

ALTER TABLE [ic].[PageSections]  WITH CHECK ADD  CONSTRAINT [FK_PageSections_SectionTypes] FOREIGN KEY([SectionType])
REFERENCES [ic].[PageSectionTypes] ([Id])
GO

ALTER TABLE [ic].[PageSections] CHECK CONSTRAINT [FK_PageSections_SectionTypes]
GO


