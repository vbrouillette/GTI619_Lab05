INSERT INTO [dbo].[AuthentificationConfig] ([Id], [IsPeriodic], [IsUpperCase], [IsLowerCase], [IsSpecialCase], [IsNumber], [PeriodPeriodic], [MaxLenght], [MinLenght], [TimeOutSession], [NbrLastPasswords]) VALUES (N'dd301ae6-0e1f-4e52-a7c2-bbee44019931', 0, 1, 1, 0, 1, 30, 25, 8, 20, 3);

SET IDENTITY_INSERT [dbo].[LoginConfig] ON
INSERT INTO [dbo].[LoginConfig] ([Id], [DelayBetweenBlocks], [NbAttemptsBeforeBlocking], [MaxBlocksBeforeAdmin], [DelayBetweenFailedAuthentication]) VALUES (1, N'2,4,6', 3, 3, 5)
SET IDENTITY_INSERT [dbo].[LoginConfig] OFF