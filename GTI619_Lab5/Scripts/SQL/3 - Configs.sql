INSERT INTO [dbo].[AuthentificationConfig] ([Id], [IsPeriodic], [IsUpperCase], [IsLowerCase], [IsSpecialCase], [IsNumber], [PeriodPeriodic], [MaxLenght], [MinLenght], [TimeOutSession], [NbrLastPasswords]) VALUES (N'dd301ae6-0e1f-4e52-a7c2-bbee44019931', 0, 1, 1, 0, 1, 30, 25, 8, 20, 3);

SET IDENTITY_INSERT [dbo].[LoginConfig] ON
INSERT INTO [dbo].[LoginConfig] ([Id], [DelayBetweenBlocks], [NbAttemptsBeforeBlocking], [MaxBlocksBeforeAdmin], [DelayBetweenFailedAuthentication]) VALUES (1, N'2,4,6', 3, 3, 5)
SET IDENTITY_INSERT [dbo].[LoginConfig] OFF

-- Admin : u(Administrateur) p(Administrateur123!)
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [PasswordHash], [SecurityStamp], [NeedNewPassword], [Discriminator]) VALUES (N'd683e5f9-ee83-4928-a3dd-fc14c1e7b919', N'Administrateur', N'AJn60PsThtriA7+PlCLNArMxKkHY1WNKA2AKWt4crSR2xJ1RRHz3CzuCMZinxLjyRA==', N'6da2755c-b6fa-49b8-b16c-0f86e12c3665', 0, N'ApplicationUser');
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd683e5f9-ee83-4928-a3dd-fc14c1e7b919', N'68e5a8c3-a1b5-423b-9a2d-200cde18c6f8');