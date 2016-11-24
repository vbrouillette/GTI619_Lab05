namespace GTI619_Lab5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateConfig : DbMigration
    {
        public override void Up()
        {
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
        }
    }
}
