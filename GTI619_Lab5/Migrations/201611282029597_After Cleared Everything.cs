namespace GTI619_Lab5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AfterClearedEverything : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthentificationConfig",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsPeriodic = c.Boolean(nullable: false),
                        PeriodPeriodic = c.Int(nullable: false),
                        NbrLastPasswords = c.Int(nullable: false),
                        MaxLenght = c.Int(nullable: false),
                        MinLenght = c.Int(nullable: false),
                        IsUpperCase = c.Boolean(nullable: false),
                        IsLowerCase = c.Boolean(nullable: false),
                        IsSpecialCase = c.Boolean(nullable: false),
                        IsNumber = c.Boolean(nullable: false),
                        TimeOutSession = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoginConfig",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DelayBetweenBlocks = c.String(),
                        NbAttemptsBeforeBlocking = c.Int(nullable: false),
                        MaxBlocksBeforeAdmin = c.Int(nullable: false),
                        DelayBetweenFailedAuthentication = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLoginLog",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userId = c.String(),
                        loginTime = c.DateTime(nullable: false),
                        success = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    UserName = c.String(),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    NeedNewPassword = c.Boolean(nullable: false, defaultValue: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserLoginLog");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.LoginConfig");
            DropTable("dbo.AuthentificationConfig");
        }
    }
}
