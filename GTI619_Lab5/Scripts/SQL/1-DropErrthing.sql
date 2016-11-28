DELETE FROM __MigrationHistory;
DROP TABLE AspNetUserRoles;
DROP TABLE AspNetUserLogins;
DROP TABLE AspNetUserClaims;
DROP TABLE AspNetRoles;
DROP TABLE AspNetUsers;
DROP TABLE AuthentificationConfig;
DROP TABLE LoginConfig;
DROP TABLE UserLoginLog;

-- Admin : u(Administrateur) p(Administrateur123!)
INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [PasswordHash], [SecurityStamp], [NeedNewPassword], [Discriminator]) VALUES (N'd683e5f9-ee83-4928-a3dd-fc14c1e7b919', N'Administrateur', N'AJn60PsThtriA7+PlCLNArMxKkHY1WNKA2AKWt4crSR2xJ1RRHz3CzuCMZinxLjyRA==', N'6da2755c-b6fa-49b8-b16c-0f86e12c3665', 0, N'ApplicationUser')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd683e5f9-ee83-4928-a3dd-fc14c1e7b919', N'68e5a8c3-a1b5-423b-9a2d-200cde18c6f80')
