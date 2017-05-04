namespace HomeKitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.RecipeIngredients", name: "ReceipeId", newName: "RecipeId");
            RenameIndex(table: "dbo.RecipeIngredients", name: "IX_ReceipeId", newName: "IX_RecipeId");
            DropPrimaryKey("dbo.RecipeIngredients");
            AddColumn("dbo.RecipeIngredients", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.RecipeIngredients", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.RecipeIngredients");
            DropColumn("dbo.RecipeIngredients", "Id");
            AddPrimaryKey("dbo.RecipeIngredients", new[] { "IngredientId", "ReceipeId" });
            RenameIndex(table: "dbo.RecipeIngredients", name: "IX_RecipeId", newName: "IX_ReceipeId");
            RenameColumn(table: "dbo.RecipeIngredients", name: "RecipeId", newName: "ReceipeId");
        }
    }
}
