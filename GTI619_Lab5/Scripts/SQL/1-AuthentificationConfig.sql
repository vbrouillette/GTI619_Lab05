CREATE TABLE [dbo].[AuthentificationConfig]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [NbrTry] INT NULL,
	[TryDownPeriod] INT NULL,
	[IsBlockAfterTwoTries] BIT NULL,
	[IsPeriodic] BIT NULL,
	[PeriodPeriodic] INT NULL,
	[MaxLenght] INT NULL,
	[MinLenght] INT NULL,
	[IsUpperCase] BIT NULL,
	[IsLowerCase] BIT NULL,
	[IsSpecialCase] BIT NULL,
	[IsNumber] BIT NULL,
	[TimeOutSession] INT NULL
)


INSERT INTO [dbo].[AuthentificationConfig] ([Id], [NbrTry], [TryDownPeriod], [IsBlockAfterTwoTries], [IsPeriodic], [PeriodPeriodic], [MaxLenght], [MinLenght], [IsUpperCase], [IsLowerCase], [IsSpecialCase], [IsNumber], [TimeOutSession]) VALUES (N'9f47dca0-02c0-4deb-8bfb-ac4ba34f31ca', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 20)
