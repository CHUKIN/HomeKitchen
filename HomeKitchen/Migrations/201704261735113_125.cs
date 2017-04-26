namespace HomeKitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _125 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TagRecipes");
            AddColumn("dbo.TagRecipes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TagRecipes", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TagRecipes");
            DropColumn("dbo.TagRecipes", "Id");
            AddPrimaryKey("dbo.TagRecipes", new[] { "RecipeId", "TagId" });
        }
    }
}
