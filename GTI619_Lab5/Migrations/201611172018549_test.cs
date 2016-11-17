namespace GTI619_Lab5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuthentificationConfig", "NbrTry", c => c.Int(nullable: false));
            AddColumn("dbo.AuthentificationConfig", "TryDownPeriod", c => c.Int(nullable: false));
            AddColumn("dbo.AuthentificationConfig", "PeriodPeriodic", c => c.Int(nullable: false));
            AddColumn("dbo.AuthentificationConfig", "MaxLenght", c => c.Int(nullable: false));
            AddColumn("dbo.AuthentificationConfig", "MinLenght", c => c.Int(nullable: false));
            AddColumn("dbo.AuthentificationConfig", "TimeOutSession", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuthentificationConfig", "TimeOutSession");
            DropColumn("dbo.AuthentificationConfig", "MinLenght");
            DropColumn("dbo.AuthentificationConfig", "MaxLenght");
            DropColumn("dbo.AuthentificationConfig", "PeriodPeriodic");
            DropColumn("dbo.AuthentificationConfig", "TryDownPeriod");
            DropColumn("dbo.AuthentificationConfig", "NbrTry");
        }
    }
}
