CREATE TABLE PageSectionTypes
(
    Id INT NOT NULL CONSTRAINT PK_PageSectionTypes PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

INSERT INTO PageSectionTypes (Id, Name) VALUES
(1, 'Text'),
(2, 'Code'),
(3, 'InfoBox'),
(4, 'Comparison'),
(5, 'InterviewQuestions');
