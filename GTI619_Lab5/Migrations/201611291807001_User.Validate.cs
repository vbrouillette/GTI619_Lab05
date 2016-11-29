namespace GTI619_Lab5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserValidate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Validated", c => c.Boolean(defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Validated");
        }
    }
}
