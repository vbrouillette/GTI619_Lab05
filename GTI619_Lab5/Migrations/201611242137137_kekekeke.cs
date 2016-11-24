namespace GTI619_Lab5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kekekeke : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.AuthentificationConfig", "NbrLastPasswords", c => c.Int(nullable: false));
            DropColumn("dbo.AuthentificationConfig", "NbrTry");
            DropColumn("dbo.AuthentificationConfig", "TryDownPeriod");
            DropColumn("dbo.AuthentificationConfig", "IsBlockAfterTwoTries");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuthentificationConfig", "IsBlockAfterTwoTries", c => c.Boolean(nullable: false));
            AddColumn("dbo.AuthentificationConfig", "TryDownPeriod", c => c.Int(nullable: false));
            AddColumn("dbo.AuthentificationConfig", "NbrTry", c => c.Int(nullable: false));
            DropColumn("dbo.AuthentificationConfig", "NbrLastPasswords");
            DropTable("dbo.UserLoginLog");
        }
    }
}
