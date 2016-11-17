
go
CREATE TABLE [dbo].[AuthentificationConfig]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [NbrTry] INT NULL,
	[TryDownPeriod] INT NULL,
	[IsBlockAfterTwoTries] BIT DEFAULT 0,
	[IsPeriodic] BIT DEFAULT 0,
	[PeriodPeriodic] INT NULL,
	[MaxLenght] INT NULL,
	[MinLenght] INT NULL,
	[IsUpperCase] BIT DEFAULT 0,
	[IsLowerCase] BIT DEFAULT 0,
	[IsSpecialCase] BIT DEFAULT 0,
	[IsNumber] BIT DEFAULT 0,
	[TimeOutSession] INT NULL
)
go
INSERT INTO [dbo].[AuthentificationConfig] ([Id], [NbrTry], [TryDownPeriod], [IsBlockAfterTwoTries], [IsPeriodic], [PeriodPeriodic], [MaxLenght], [MinLenght], [IsUpperCase], [IsLowerCase], [IsSpecialCase], [IsNumber], [TimeOutSession]) VALUES (N'9f47dca0-02c0-4deb-8bfb-ac4ba34f31ca', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 20)
