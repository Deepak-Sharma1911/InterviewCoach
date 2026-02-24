CREATE SCHEMA ic;

go
USE [InterviewCoachDB]
GO

/****** Object:  Table [ic].[Technology]    Script Date: 2/13/2026 4:57:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [ic].[Technology](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Slug] [nvarchar](200) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
    [RowVersion] rowversion NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedUtcDate] [datetime2](7) NOT NULL,
	[LastModifiedBy] [uniqueidentifier] NULL,
	[LastUtcModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Technologies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [ic].[Technology] ADD  DEFAULT ((0)) FOR [DisplayOrder]
GO

ALTER TABLE [ic].[Technology] ADD  DEFAULT ((1)) FOR [IsActive]
go

CREATE UNIQUE INDEX index_name
ON [ic].[Technology] ([Slug]);



