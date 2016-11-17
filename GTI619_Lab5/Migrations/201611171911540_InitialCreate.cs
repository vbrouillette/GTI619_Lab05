namespace GTI619_Lab5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthentificationConfig",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsBlockAfterTwoTries = c.Boolean(nullable: false),
                        IsPeriodic = c.Boolean(nullable: false),
                        IsUpperCase = c.Boolean(nullable: false),
                        IsLowerCase = c.Boolean(nullable: false),
                        IsSpecialCase = c.Boolean(nullable: false),
                        IsNumber = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuthentificationConfig");
        }
    }
}
