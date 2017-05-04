namespace HomeKitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _256 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "Hours", c => c.Int(nullable: false));
            AddColumn("dbo.Recipes", "Minutes", c => c.Int(nullable: false));
            DropColumn("dbo.Recipes", "CookingTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recipes", "CookingTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Recipes", "Minutes");
            DropColumn("dbo.Recipes", "Hours");
        }
    }
}
