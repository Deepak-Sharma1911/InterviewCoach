USE [InterviewCoachDB]
GO

/****** Object:  Table [ic].[PageSectionTypes]    Script Date: 2/13/2026 4:53:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [ic].[PageSectionTypes](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PageSectionTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


INSERT INTO ic.PageSectionTypes (Id, Name) VALUES
(1, 'Text'),
(2, 'Code'),
(3, 'InfoBox'),
(4, 'Comparison'),
(5, 'InterviewQuestions');
